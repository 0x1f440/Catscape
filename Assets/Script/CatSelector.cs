using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSelector : MonoBehaviour
{
    public static bool isRandom { 
        get { 
            return DataManager.Instance.equipCategory == 
                "Common" && DataManager.Instance.equipNumber == 0; 
        } 
    }

    public static int normalCount = 31;
    public static int rareCount = 32;
    public static int specialCount = 10;
    public static int CatCount { get { return normalCount + rareCount + specialCount; } }

    public static int rescuedCatNumber;
    public static string rescuedCatCategory;

    void OnEnable()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cats/"+ DataManager.Instance.equipCategory +"/" + DataManager.Instance.equipNumber.ToString());
    }

    public static void GetRandomCat()
    {
        if (GameManager.stage < 20)
        {
            LoadCommonCat();
            return;
        }

        int dice = Random.Range(0, 100);
        if (dice < 90)
        {
            LoadCommonCat();
        }
        else if (dice < 97)
        {
            LoadRareCat();
        }
        else
        {
            LoadSpecialCat();
        }
    }

    static void LoadCommonCat()
    {
        rescuedCatCategory = "Common";
        rescuedCatNumber = Random.Range(1, normalCount);
    }

    static void LoadRareCat()
    {
        rescuedCatCategory = "Rare";
        int random = Random.Range(0, DataManager.Instance.unlockedRareCats.Count);
        rescuedCatNumber = DataManager.Instance.unlockedRareCats[random];
    }

    static void LoadSpecialCat()
    {
        rescuedCatCategory = "Special";
        int random = Random.Range(0, DataManager.Instance.unlockedSpecialCats.Count);
        rescuedCatNumber = DataManager.Instance.unlockedSpecialCats[random];
    }
}
