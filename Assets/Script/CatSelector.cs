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
        }
        else if(GameManager.stage < 20)
        {
            catCategory = "Common";
            catNumber = Random.Range(0, normalCount);
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cats/" + catCategory + "/" + catNumber.ToString());
        }   
    }
}
