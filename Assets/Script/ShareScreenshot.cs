using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareScreenshot : MonoBehaviour
{
    public void Share()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(TakeScreenshotAndSave());
    }
    private IEnumerator TakeScreenshotAndSave()
    {
        string path = "";
        yield return new WaitForEndOfFrame();

        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);

        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();

        //Convert to png
        byte[] imageBytes = screenImage.EncodeToPNG();


        System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/GameOverScreenShot");
        path = Application.persistentDataPath + "/GameOverScreenShot" + "/DiedScreenShot.png";
        System.IO.File.WriteAllBytes(path, imageBytes);

        StartCoroutine(ShareScreenshotRoutine(path));
    }

    private IEnumerator ShareScreenshotRoutine(string destination)
    {
        var jsonData = Resources.Load<TextAsset>("Cats/"+CatSelector.rescuedCatCategory+"/" + CatSelector.rescuedCatNumber.ToString());
        var catData = JsonUtility.FromJson<CatData>(jsonData.text);

        string ShareSubject = "Picture Share";
        string textToShare = "공원에서 길을 잃은 고양이를 구했어요! " +
            "<"+ catData.name +" : " + catData.description + ">\n";
        string shareLink = "storelink";

        Debug.Log(destination);


        if (!Application.isEditor)
        {

            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);

            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), textToShare + shareLink);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), ShareSubject);
            intentObject.Call<AndroidJavaObject>("setType", "image/png");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("startActivity", intentObject);
        }

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetACat("Rare",7);

        yield return null;
    }
}
