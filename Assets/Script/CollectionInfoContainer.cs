using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionInfoContainer : MonoBehaviour
{
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
            DetailPage.GetComponent<CollectionDetailPage>().OpenDetailPage(catName, catDesc, catImg);
        }
        else
        {
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
