using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionDetailPage : MonoBehaviour
{
    GameObject detailPage;
    Text catNameUI;
    Text catDescriptionUI;
    Image catImgUI;
    public GameObject collectionBg;

    public bool isLocked;
    public bool isRescued;
    
    // Start is called before the first frame update
    void FetchVariables()
    {
        detailPage = gameObject;
        catNameUI = detailPage.transform.Find("Name").GetComponent<Text>();
        catDescriptionUI = detailPage.transform.Find("Desc").GetComponent<Text>();
        catImgUI = detailPage.transform.Find("Picture").GetComponent<Image>();
    }

    private void OnEnable()
    {
        collectionBg.SetActive(false);

    }

    private void OnDisable()
    {
        collectionBg.SetActive(true);
    }
    public void OpenDetailPage(string catName, string catDesc, Sprite catImg)
    {
        if (catNameUI == null)
            FetchVariables();

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

    public void EquipCatSkin()
    {

    }
    public void UnequipCatSkin()
    {

    }
}
