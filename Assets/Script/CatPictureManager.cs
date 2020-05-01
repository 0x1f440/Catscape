using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatPictureManager : MonoBehaviour
{
    private void OnEnable()
    {
        transform.Find("Photo/Cat").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cats/" + CatSelector.catCategory + "/" + CatSelector.catNumber.ToString());

        var newRibbon = transform.Find("NewRibbon").gameObject;
        switch (CatSelector.catCategory)
        {
            case "Common":
                if (DataManager.Instance.rescuedCommonCats.Contains(CatSelector.catNumber))
                {
                    newRibbon.SetActive(false);
                }
                else
                {
                    newRibbon.SetActive(true);
                }
                break;
            case "Rare":
                if (DataManager.Instance.rescuedRareCats.Contains(CatSelector.catNumber))
                {
                    newRibbon.SetActive(false);
                }
                else
                {
                    newRibbon.SetActive(true);
                }
                break;
            case "Special":
                if (DataManager.Instance.rescuedSpecialCats.Contains(CatSelector.catNumber))
                {
                    newRibbon.SetActive(false);
                }
                else
                {
                    newRibbon.SetActive(true);
                }
                break;
        }

    }
}
