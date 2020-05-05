using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRepository : MonoBehaviour
{
    private static string[] levels;
    public static int numberOfLevels
    {
        get
        {
            return levels.Length;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        levels = Resources.Load<TextAsset>("levels").text.TrimEnd().TrimEnd(',').Split(',');
    }

    public string[] GetLevels()
    {
        return levels;
    }
}
