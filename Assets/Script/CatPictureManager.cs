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

        if (CatSelector.isRandom)
        {
            string path = "Cats/" + CatSelector.rescuedCatCategory + "/" + CatSelector.rescuedCatNumber.ToString();
            var jsonData = Resources.Load<TextAsset>(path);
            var catData = JsonUtility.FromJson<CatData>(jsonData.text);

            transform.Find("Photo/Cat").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
            transform.Find("Text").GetComponent<Text>().text = catData.name;
        }
        else
        {
            string path = "Cats/" + DataManager.Instance.equipCategory + "/" + DataManager.Instance.equipNumber.ToString();
            var jsonData = Resources.Load<TextAsset>(path);
            var catData = JsonUtility.FromJson<CatData>(jsonData.text);

            transform.Find("Photo/Cat").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
            transform.Find("Text").GetComponent<Text>().text = catData.name;
        }
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
