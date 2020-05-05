using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseManager : MonoBehaviour
{
    bool isBuying = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.Find("Price").GetComponent<Text>().text = CollectionDetailPage.catData.price.ToString();
        if (CollectionDetailPage.catData.price <= DataManager.Instance.meowney)
        {
            GetComponent<Image>().color = new Color(1, 0.97f, 0.72f);
            transform.Find("Purchase text").GetComponent<Text>().text = "구매하기";
        }
        else
        {
            GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
            transform.Find("Purchase text").GetComponent<Text>().text = "먀니 부족";
        }
    }

    public void PurchaseACat()
    {
        if(CollectionDetailPage.catData.price <= DataManager.Instance.meowney)
        {
            CollectionDetailPage.isRescued = true;

            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().LoseMeowney(CollectionDetailPage.catData.price);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetACat(CollectionDetailPage.selectedCatCategory, CollectionDetailPage.selectedCatNumber);
            
            transform.parent.Find("Picture").GetComponent<Image>().color = new Color(1, 1, 1);
            transform.parent.Find("Name").GetComponent<Text>().text = CollectionDetailPage.catData.name;
            transform.parent.Find("Desc").GetComponent<Text>().text = CollectionDetailPage.catData.description;
            
            transform.parent.Find("Equip").GetComponent<EquipCatManager>().BeEqippable();
            transform.parent.Find("Equip").gameObject.SetActive(true);

            gameObject.SetActive(false);

        }
    }
 }
