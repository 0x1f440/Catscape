using System;
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

    private void UpdateCatData()
    {
        int count = 0;

        while (count < CATS_PER_PAGE)
        {
            var info = slot.transform.GetChild(count).GetComponent<CollectionInfoContainer>();
            var catImg = slot.transform.GetChild(count).Find("ColCatImg").GetComponent<Image>();
            var catName = slot.transform.GetChild(count).Find("Name").GetComponent<Text>();
            var lockImg = slot.transform.GetChild(count).Find("LockImage").gameObject;

            int catNumber = index * CATS_PER_PAGE + count;

            TextAsset jsonData = null;
            CatData catData = null;

            catImg.color = new Color(1, 1, 1);
            lockImg.SetActive(false);

            info.hasCat = true;
            info.isLocked = false;
            if (catNumber < CatSelector.normalCount)
            {
                info.catNumber = catNumber;
                info.catCategory = "Common";
                
                catImg.sprite = Resources.Load<Sprite>("Cats/Common/" + catNumber.ToString());
                if (DataManager.Instance.rescuedCommonCats.Contains(catNumber))
                {
                    jsonData = Resources.Load<TextAsset>("Cats/Common/" + catNumber.ToString());
                    catData = JsonUtility.FromJson<CatData>(jsonData.text);
                    catName.text = catData.name;
                }
                else
                {
                    info.hasCat = false;
                    catImg.color = new Color(0, 0, 0);
                    catName.text = "???";
                }
            }
            else if (catNumber < CatSelector.normalCount + CatSelector.rareCount)
            {
                info.catNumber = catNumber;
                info.catCategory = "Rare";

                catNumber -= CatSelector.normalCount;
                catImg.sprite = Resources.Load<Sprite>("Cats/Rare/" + catNumber.ToString());

                jsonData = Resources.Load<TextAsset>("Cats/Rare/" + catNumber.ToString());
                catData = JsonUtility.FromJson<CatData>(jsonData.text);

                if (DataManager.Instance.rescuedRareCats.Contains(catNumber))
                {
                    catName.text = catData.name;
                }
                else
                {
                    info.hasCat = false;
                    catImg.color = new Color(0, 0, 0);
                    if (DataManager.Instance.unlockedRareCats.Contains(catNumber))
                    {
                        catName.text = "???";
                    }
                    else
                    {
                        info.isLocked = true;
                        catName.text = catData.unlockCondition;
                        lockImg.SetActive(true);
                    }
                }
            }
            else if (catNumber < CatSelector.normalCount + CatSelector.rareCount + CatSelector.specialCount)
            {
                info.catNumber = catNumber;
                info.catCategory = "Special";

                catNumber -= CatSelector.normalCount + CatSelector.rareCount;
                catImg.sprite = Resources.Load<Sprite>("Cats/Special/" + catNumber.ToString());

                jsonData = Resources.Load<TextAsset>("Cats/Special/" + catNumber.ToString());
                catData = JsonUtility.FromJson<CatData>(jsonData.text);
                
                if (DataManager.Instance.rescuedSpecialCats.Contains(catNumber))
                {
                    catName.text = catData.name;
                }
                else
                {
                    info.hasCat = false;
                    catImg.color = new Color(0, 0, 0);
                    if (DataManager.Instance.unlockedSpecialCats.Contains(catNumber))
                    {
                        catName.text = "???";
                    }
                    else
                    {
                        info.isLocked = true;
                        catName.text = catData.unlockCondition;
                        lockImg.SetActive(true);
                    }
                }
            }
            else
            {
                info.hasCat = false;
                catImg.sprite = null;
                catName.text = "";
            }

            info.catName = catName.text;
            info.catImg = catImg.sprite;

            if (catData != null)
            {
                info.catDesc = catData.description;
                info.catUnlockCondition = catData.unlockCondition;
            }
            else
            {
                info.catDesc = "";
                info.catUnlockCondition = "";
            }
  
            count++;
        }
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
