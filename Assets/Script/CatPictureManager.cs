using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatPictureManager : MonoBehaviour
{
    public static bool showRibbon;
    private void OnEnable()
    {
        CheckRibbon();

        string path;

        if (CatSelector.isRandom)
        {
            path = "Cats/" + CatSelector.rescuedCatCategory + "/" + CatSelector.rescuedCatNumber.ToString();
        }
        else
        {
            path = "Cats/" + DataManager.Instance.equipCategory + "/" + DataManager.Instance.equipNumber.ToString();
        }

        var jsonData = Resources.Load<TextAsset>(path);
        var catData = JsonUtility.FromJson<CatData>(jsonData.text);

        transform.Find("Photo/Cat").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        transform.Find("Text").GetComponent<Text>().text = catData.name;
        transform.Find("SpeechBubble/Desc").GetComponent<Text>().text = catData.description;
    }

    private void CheckRibbon()
    {
        var newRibbon = transform.Find("NewRibbon").gameObject;
        if (showRibbon)
        {
            newRibbon.SetActive(true);
        }
        else
        {
            newRibbon.SetActive(false);
        }
    }
}
