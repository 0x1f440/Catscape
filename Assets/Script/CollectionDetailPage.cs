using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionDetailPage : MonoBehaviour
{
    public static int selectedCatNumber;
    public static string selectedCatCategory;

    GameObject detailPage;
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
    }
    public void OpenDetailPage(string catName, string catDesc, Sprite catImg, bool isEquipedBool)
    {
        if (catNameUI == null)
            FetchVariables();

        isEquipped = isEquipedBool;

        catNameUI.text = catName;
        catDescriptionUI.text = catDesc;
        catImgUI.sprite = catImg;
        catImgUI.color = new Color(1, 1, 1);
        gameObject.SetActive(true);
    }

    public void OpenDetailPage(Sprite catImg)
    {
        if (catNameUI == null)
            FetchVariables();

        isEquipped = false;

        catNameUI.text = "???";
        catImgUI.sprite = catImg;
        catImgUI.color = new Color(0, 0, 0);

        catDescriptionUI.text = "???";


        gameObject.SetActive(true);
    }
    public void OpenDetailPage(string catUnlockCondition, Sprite catImg)
    {
        if (catNameUI == null)
            FetchVariables();

        isEquipped = false;

        catNameUI.text = "???";
        catImgUI.sprite = catImg;
        catImgUI.color = new Color(0, 0, 0);
        
        catDescriptionUI.text = catUnlockCondition;       

        gameObject.SetActive(true);
    }

    public void CloseDetailPage()
    {
        gameObject.SetActive(false);
    }
}
