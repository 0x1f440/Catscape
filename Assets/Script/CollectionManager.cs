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
        UpdatePercentUI();
        UpdateCatData();
        UpdateIndexUI();
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

            var catImg = slot.transform.GetChild(count).Find("Image").GetComponent<Image>();
            var catName = slot.transform.GetChild(count).Find("Name").GetComponent<Text>();
            int catNumber = index * CATS_PER_PAGE + count;

            catImg.color = new Color(1, 1, 1);
                
            if (catNumber < CatSelector.normalCount)
            {
                catImg.sprite = Resources.Load<Sprite>("Cats/Common/" + catNumber.ToString());
                if (DataManager.Instance.rescuedCommonCats.Contains(catNumber))
                {
                    var jsonData = Resources.Load<TextAsset>("Cats/Common/" + catNumber.ToString());
                    var catData = JsonUtility.FromJson<CatData>(jsonData.text);
                    catName.text = catData.name;
                }
                else
                {
                    catImg.color = new Color(0, 0, 0);
                    catName.text = "???";
                }
            }
            else if (catNumber < CatSelector.normalCount + CatSelector.rareCount)
            {
                catNumber -= CatSelector.normalCount;
                catImg.sprite = Resources.Load<Sprite>("Cats/Rare/" + catNumber.ToString());

                var jsonData = Resources.Load<TextAsset>("Cats/Rare/" + catNumber.ToString());
                var catData = JsonUtility.FromJson<CatData>(jsonData.text);
                
                if (DataManager.Instance.rescuedRareCats.Contains(catNumber))
                {
                    catName.text = catData.name;
                }
                else
                {
                    catImg.color = new Color(0, 0, 0);
                    if (DataManager.Instance.openedRareCats.Contains(catNumber))
                    {
                        catName.text = "???";
                    }
                    else
                    {
                        catName.text = catData.unlockCondition;
                    }
                }
            }
            else if (catNumber < CatSelector.normalCount + CatSelector.rareCount + CatSelector.specialCount)
            {
                catNumber -= CatSelector.normalCount + CatSelector.rareCount;
                catImg.sprite = Resources.Load<Sprite>("Cats/Special/" + catNumber.ToString());

                var jsonData = Resources.Load<TextAsset>("Cats/Special/" + catNumber.ToString());
                var catData = JsonUtility.FromJson<CatData>(jsonData.text);
                
                if (DataManager.Instance.rescuedSpecialCats.Contains(catNumber))
                {
                    catName.text = catData.name;
                }
                else
                {
                    catImg.color = new Color(0, 0, 0);
                    if (DataManager.Instance.openedSpecialCats.Contains(catNumber))
                    {
                        catName.text = "???";
                    }
                    else
                    {
                        catName.text = catData.unlockCondition;
                    }
                }
            }
            else
            {
                catImg.sprite = null;
                catName.text = "";
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
