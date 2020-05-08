using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Globalization;

[Serializable]
public class DataManager
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataManager();
                instance.Create();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public int stage;
    public int meowney;

    public List<int> rescuedCommonCats;
    public List<int> rescuedRareCats;
    public List<int> rescuedSpecialCats;

    public List<int> unlockedRareCats;
    public List<int> unlockedSpecialCats;

    public string equipCategory;
    public int equipNumber;

    public int seenAd;
    

    private void Create()
    {
        instance.stage = 0;
        instance.meowney = 0;

        instance.rescuedCommonCats = new List<int> { 0 };
        instance.rescuedRareCats = new List<int> { };
        instance.rescuedSpecialCats = new List<int> { };

        instance.unlockedRareCats = new List<int> { 0, 2, 6, 11, 15, 25, 26, 28, 31 };
        instance.unlockedSpecialCats = new List<int> { 3, 7 };

        instance.equipCategory = "Common";
        instance.equipNumber = 0;

        instance.seenAd = 0;
    }

    public static void Save()
    {
        string path = Path.Combine(Application.persistentDataPath, "save");
        var savedata = JsonUtility.ToJson(Instance);
        File.WriteAllText(path, savedata, Encoding.UTF8);
    }
    
    public static void Load()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "save")))
        {
            string path = Path.Combine(Application.persistentDataPath, "save");
            var data = File.ReadAllText(path, Encoding.UTF8);
            Instance = JsonUtility.FromJson<DataManager>(data);
        }

    }
}

