﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    const int CATS_PER_PAGE = 8;
    static int index = 0;
    static int maxIndex = (int)Mathf.Ceil(CatSelector.CatCount / CATS_PER_PAGE)+1;
    public GameObject prevBtn, nextBtn, completeUI, indexUI, slot, percentUI;

    public Sprite lockDefault, lockPurchase;

    CollectionInfoContainer infoSlot;
    Image catImg;
    Text catName;
    GameObject lockImg;
    CatData catData;
    TextAsset jsonData;
    void OnEnable()
    {
        GameManager.isCollectionOpen = true;
        UpdatePercentUI();
        UpdateCatData();
        UpdateIndexUI();
    }

    private void OnDisable()
    {
        GameManager.isCollectionOpen = false;
    }

    private void UpdatePercentUI()
    {
        int openedCats = DataManager.Instance.rescuedCommonCats.Count + DataManager.Instance.rescuedRareCats.Count + DataManager.Instance.rescuedSpecialCats.Count;
        percentUI.GetComponent<Text>().text = (((float)openedCats / CatSelector.CatCount) * 100).ToString("F1", new CultureInfo("en-US")) + "% COMPLETE";

    }

    public void ToPrevPage()
    {
        index = (index + maxIndex - 1) % maxIndex;
        UpdateCatData();
        UpdateIndexUI();
    }

    public void ToNextPage()
    {
        index = (index + 1) % maxIndex;
        UpdateCatData();
        UpdateIndexUI();
    }

    public void UpdateCatData()
    {
        int count = 0;

        while (count < CATS_PER_PAGE)
        {
            infoSlot = slot.transform.GetChild(count).GetComponent<CollectionInfoContainer>();
            catImg = slot.transform.GetChild(count).Find("ColCatImg").GetComponent<Image>();
            catName = slot.transform.GetChild(count).Find("Name").GetComponent<Text>();
            lockImg = slot.transform.GetChild(count).Find("LockImage").gameObject;

            int catNumber = index * CATS_PER_PAGE + count;

            jsonData = null;
            catData = null;

            catImg.color = new Color(1, 1, 1);
            lockImg.SetActive(false);

            infoSlot.hasCat = true;
            infoSlot.isLocked = false;

            if (catNumber < CatSelector.normalCount)
            {
                infoSlot.catNumber = catNumber;
                infoSlot.catCategory = "Common";
                catImg.sprite = Resources.Load<Sprite>("Cats/Common/" + catNumber.ToString());
                
                if (CheckIfHasCat("Common", catNumber))
                {
                    jsonData = Resources.Load<TextAsset>("Cats/Common/" + catNumber.ToString());
                    catData = JsonUtility.FromJson<CatData>(jsonData.text);
                    catName.text = catData.name;
                }
                else
                {
                    infoSlot.hasCat = false;
                    catImg.color = new Color(0, 0, 0);
                    catName.text = "???";
                }
            }
            else if (catNumber < CatSelector.normalCount + CatSelector.rareCount)
            {

                catNumber -= CatSelector.normalCount;
                catImg.sprite = Resources.Load<Sprite>("Cats/Rare/" + catNumber.ToString());

                infoSlot.catNumber = catNumber;
                infoSlot.catCategory = "Rare";

                jsonData = Resources.Load<TextAsset>("Cats/Rare/" + catNumber.ToString());
                catData = JsonUtility.FromJson<CatData>(jsonData.text);

                if (CheckIfHasCat("Rare", catNumber))
                {
                    catName.text = catData.name;
                }
                else
                {
                    infoSlot.hasCat = false;
                    catImg.color = new Color(0, 0, 0);
                    if (DataManager.Instance.unlockedRareCats.Contains(catNumber))
                    {
                        catName.text = "???";
                    }
                    else
                    {
                        SetLockedCatSlot();    
                    }
                }
            }
            else if (catNumber < CatSelector.normalCount + CatSelector.rareCount + CatSelector.specialCount)
            {
                catNumber -= CatSelector.normalCount + CatSelector.rareCount;
                catImg.sprite = Resources.Load<Sprite>("Cats/Special/" + catNumber.ToString());

                infoSlot.catNumber = catNumber;
                infoSlot.catCategory = "Special";

                jsonData = Resources.Load<TextAsset>("Cats/Special/" + catNumber.ToString());
                catData = JsonUtility.FromJson<CatData>(jsonData.text);
                
                if (CheckIfHasCat("Special", catNumber))
                {
                    catName.text = catData.name;
                }
                else
                {
                    infoSlot.hasCat = false;
                    catImg.color = new Color(0, 0, 0);
                    if (DataManager.Instance.unlockedSpecialCats.Contains(catNumber))
                    {
                        catName.text = "???";
                    }
                    else
                    {
                        SetLockedCatSlot();
                    }
                }
            }
            else
            {
                SetEmptySlot();
            }

            infoSlot.catImg = catImg.sprite;
            infoSlot.catData = catData;
  
            count++;
        }
    }

    bool CheckIfHasCat(string category, int number)
    {
        switch (category)
        {
            case "Common":
                return DataManager.Instance.rescuedCommonCats.Contains(number);

            case "Rare":
                return DataManager.Instance.rescuedRareCats.Contains(number);

            case "Special":
                return DataManager.Instance.rescuedSpecialCats.Contains(number);
        }

        return false;
    }

    void SetEmptySlot()
    {
        infoSlot.hasCat = false;
        catImg.sprite = null;
        catName.text = "";
    }
    void SetLockedCatSlot()
    {
        infoSlot.isLocked = true;
        catName.text = catData.unlockCondition;

        if (catData.catType == "purchase")
        {
            lockImg.GetComponent<Image>().sprite = lockPurchase;
        }
        else
        {
            lockImg.GetComponent<Image>().sprite = lockDefault;
        }
        lockImg.SetActive(true);
    }

    void UpdateIndexUI()
    {
        indexUI.GetComponent<Text>().text = (index+1).ToString() + "/" + maxIndex.ToString();
    }

    public void OpenCollection()
    {
        gameObject.SetActive(true);
    }

    public void CloseCollection()
    {
        gameObject.SetActive(false);
    }
}
