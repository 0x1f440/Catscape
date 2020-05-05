using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipCatManager : MonoBehaviour
{
    private Color active = new Color(1, 0.97f, 0.72f);
    private Color inactive = new Color(0.4f, 0.4f, 0.4f);
    public Image catImg;
    private void OnEnable()
    {
        if (CollectionDetailPage.isEquipped)
        {
            transform.GetChild(0).GetComponent<Text>().text = "장착중";
            GetComponent<Image>().color = inactive;
        }
        else
        {
            BeEqippable();
        }
    }
   
    public void BeEqippable()
    {
        transform.GetChild(0).GetComponent<Text>().text = "장착하기";

        if (CollectionDetailPage.isRescued)
            GetComponent<Image>().color = active;
        else
            GetComponent<Image>().color = inactive;

    }
    public void Equip()
    {
        if (CollectionDetailPage.isEquipped || !CollectionDetailPage.isRescued)
        {
            return;
        }
        else
        {
            GetComponent<AudioSource>().Play();
            DataManager.Instance.equipCategory = CollectionDetailPage.selectedCatCategory;
            DataManager.Instance.equipNumber = CollectionDetailPage.selectedCatNumber;
            CollectionDetailPage.isEquipped = true;
            GameObject.FindGameObjectWithTag("Target").GetComponent<SpriteRenderer>().sprite = catImg.sprite;
            OnEnable();
        }
    }
}
