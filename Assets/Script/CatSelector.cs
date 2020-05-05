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

    public static int normalCount = 28;
    public static int rareCount = 13;
    public static int specialCount = 4;
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
        if (dice < 50)
        {
            LoadCommonCat();
        }
        else if (dice < 80)
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
        rescuedCatNumber = Random.Range(0, normalCount);
    }

    static void LoadRareCat()
    {
        rescuedCatCategory = "Rare";
        rescuedCatNumber = Random.Range(0, rareCount);
    }

    static void LoadSpecialCat()
    {
        rescuedCatCategory = "Special";
        rescuedCatNumber = Random.Range(0, specialCount);
    }
}
