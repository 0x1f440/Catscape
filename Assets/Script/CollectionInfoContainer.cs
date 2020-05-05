using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionInfoContainer : MonoBehaviour
{
    public string catCategory;
    public int catNumber;

    public CatData catData;
    public Sprite catImg;

    public bool hasCat;
    public bool isLocked;

    public GameObject DetailPage;

    public void OpenDetailPage()
    {
        if (hasCat)
        {
            CollectionDetailPage.isRescued = true;
        }
        else
        {
            CollectionDetailPage.isRescued = false;
        }
        
        CollectionDetailPage.selectedCatCategory = catCategory;
        CollectionDetailPage.selectedCatNumber = catNumber;
        CollectionDetailPage.catData = catData;
        CollectionDetailPage.isLocked = isLocked;
        DetailPage.GetComponent<CollectionDetailPage>().OpenDetailPage(catImg);
    }
}
