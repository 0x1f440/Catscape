using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

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

    public List<int> rescuedCommonCats;
    public List<int> rescuedRareCats;
    public List<int> rescuedSpecialCats;

    public List<int> openedRareCats;
    public List<int> openedSpecialCats;
    

    private void Create()
    {
        instance.stage = 0;

        instance.rescuedCommonCats = new List<int> { };
        instance.rescuedRareCats = new List<int> { };
        instance.rescuedSpecialCats = new List<int> { };

        instance.openedRareCats = new List<int> { };
        instance.openedSpecialCats = new List<int> { };
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

