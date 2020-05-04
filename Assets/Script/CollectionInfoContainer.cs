using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionInfoContainer : MonoBehaviour
{
    public string catCategory;
    public int catNumber;

    public string catName;
    public string catDesc;
    public Sprite catImg;
    public string catUnlockCondition;

    public bool hasCat;
    public bool isLocked;

    public GameObject DetailPage;

    public void OpenDetailPage()
    {
        if (hasCat)
        {
            bool isEquiped = DataManager.Instance.equipCategory == catCategory && DataManager.Instance.equipNumber == catNumber;
            CollectionDetailPage.isRescued = true;
            CollectionDetailPage.selectedCatCategory = catCategory;
            CollectionDetailPage.selectedCatNumber = catNumber;
            
            DetailPage.GetComponent<CollectionDetailPage>().OpenDetailPage(catName, catDesc, catImg, isEquiped);
        }
        else
        {
            CollectionDetailPage.isRescued = false;
            if (isLocked)
            {
                DetailPage.GetComponent<CollectionDetailPage>().OpenDetailPage(catUnlockCondition, catImg);
            }
            else
            {
                DetailPage.GetComponent<CollectionDetailPage>().OpenDetailPage(catImg);
            }
        }
    }
}
