using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSelector : MonoBehaviour
{
    public static int normalCount = 28;
    public static int rareCount = 12;
    public static int specialCount = 2;
    public static int CatCount { get { return normalCount + rareCount + specialCount; } }

    public static int catNumber;
    public static string catCategory;
    void OnEnable()
    {
        if (GameManager.isRestart)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cats/" + catCategory + "/" + catNumber.ToString());
            return;
        }
        
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

    void LoadCommonCat()
    {
        catCategory = "Common";
        catNumber = Random.Range(0, normalCount);
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cats/Common/" + catNumber.ToString());
    }

    void LoadRareCat()
    {
        catCategory = "Rare";
        catNumber = Random.Range(0, rareCount);
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cats/Rare/" + catNumber.ToString());
    }

    void LoadSpecialCat()
    {
        catCategory = "Special";
        catNumber = Random.Range(0, specialCount);
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cats/Special/" + catNumber.ToString());
    }
}
