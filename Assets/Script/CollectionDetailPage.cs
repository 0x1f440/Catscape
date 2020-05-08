using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionDetailPage : MonoBehaviour
{
    public static int selectedCatNumber;
    public static string selectedCatCategory;
    public static CatData catData;

    GameObject detailPage, equipButton;
    Text catNameUI;
    Text catDescriptionUI;
    Image catImgUI;
    public GameObject collectionBg;

    public static bool isLocked;
    public static bool isRescued;
    public static bool isEquipped;
    
    // Start is called before the first frame update
    void FetchVariables()
    {
        detailPage = gameObject;
        catNameUI = detailPage.transform.Find("Name").GetComponent<Text>();
        catDescriptionUI = detailPage.transform.Find("Desc").GetComponent<Text>();
        catImgUI = detailPage.transform.Find("Picture").GetComponent<Image>();
        equipButton = detailPage.transform.Find("Equip").gameObject;
    }
    public void OpenDetailPage(Sprite catImg)
    {
        if (catNameUI == null)
            FetchVariables();

        isEquipped = DataManager.Instance.equipCategory == selectedCatCategory && DataManager.Instance.equipNumber == selectedCatNumber;
        catImgUI.sprite = catImg;
        if (isRescued)
        {
            catNameUI.text = catData.name;
            catImgUI.color = new Color(1, 1, 1);
            equipButton.SetActive(true);

        }
        else
        {
            catNameUI.text = "???";
            catImgUI.color = new Color(0,0,0);
            equipButton.SetActive(false);
        }

        transform.Find("Purchase").gameObject.SetActive(false);
        catDescriptionUI.text = catData.description;
        
        if (isLocked && catData.catType == "purchase")
        {
            transform.Find("Purchase").gameObject.SetActive(true);
        }   

        gameObject.SetActive(true);
    }

    public void CloseDetailPage()
    {
        gameObject.SetActive(false);
    }
}
