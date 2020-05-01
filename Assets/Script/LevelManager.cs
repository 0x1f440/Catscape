using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject horizontal2, horizontal3, vertical2, vertical3, target;

    void Awake()
    {
        horizontal2 = Resources.Load("blockObjects/horizontal-2") as GameObject;
        horizontal3 = Resources.Load("blockObjects/horizontal-3") as GameObject;
        vertical2 = Resources.Load("blockObjects/vertical-2") as GameObject;
        vertical3 = Resources.Load("blockObjects/vertical-3") as GameObject;
        target = Resources.Load("blockObjects/target") as GameObject;
    }

    internal void LoadLevel(int stage)
    {
        var stages = GetComponent<LevelRepository>().GetLevels()[stage].TrimStart().Split('\n').Select(p => p.Trim()).ToArray();
        SetHorizontalBlocks(stages);
        SetVerticalBlocks(stages);
    }

    void SetHorizontalBlocks(string[] stages)
    {
        for (int i = 0; i < stages.Length; i++)
        {
            for (int j = 0; j < stages[i].Length-1; j++)
            {
                if (stages[i][j] != '.')
                {
                    if (j + 1 >= stages[i].Length ||  stages[i][j] != stages[i][j + 1])
                        continue;

                    if (j + 2 >= stages[i].Length || stages[i][j] != stages[i][j + 2])
                    {
                        Vector3 pos = new Vector3(j-2, -i+2, 0);
                        if (stages[i][j] == 'r')
                        {
                            Instantiate(target, pos, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(horizontal2, pos, Quaternion.identity);
                        }
                        j +=1 ;
                    }
                    else
                    {
                        Vector3 pos = new Vector3(j-1.5f, -i+2, 0);
                        Instantiate(horizontal3, pos, Quaternion.identity);
                        j += 2;
                    }
                    
                }
            }
        }
    }

    void SetVerticalBlocks(string[] stages)
    {
        for (int j = 0; j < stages.Length; j++)
        {
            for (int i = 0; i < stages[j].Length-1; i++)
            {
                if (stages[i][j] != '.')
                {
                    if (i + 1 > stages[0].Length || stages[i][j] != stages[i + 1][j])
                        continue;

                    if (i + 2 >= stages[0].Length|| stages[i][j] != stages[i + 2][j])
                    {
                        Vector3 pos = new Vector3(j - 2.5f, -i + 1.5f, 0);
                        Instantiate(vertical2, pos, Quaternion.Euler(0, 0, 90));
                        i += 1;
                    }
                    else
                    {
                        Vector3 pos = new Vector3(j - 2.5f, -i + 1f, 0);
                        Instantiate(vertical3, pos, Quaternion.identity);
                        i += 2;
                    }

                }
            }
        }
    }
}
