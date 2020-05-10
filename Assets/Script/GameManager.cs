using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool isCollectionOpen = false;
    public static int stage {
        get { 
            return DataManager.Instance.stage; 
        }
        set {
            DataManager.Instance.stage = value;
            DataManager.Save();
        }
    }
    public static int move = 0;
    public GameObject clearScene;

    GameObject upperUI;
    Text stageUI, moveUI, MeowneyUI;
    public Text addMeowneyUI;
    public static bool isRestart;

    public delegate void GameEndHandler();
    public static event GameEndHandler GameEndEventHandler;

    private void Start()
    {
        DataManager.Load();
        upperUI = GameObject.FindGameObjectWithTag("UpperUI");
        stageUI = upperUI.transform.Find("Stage/Content Text").GetComponent<Text>();
        moveUI = upperUI.transform.Find("Moves/Content Text").GetComponent<Text>();
        MeowneyUI = upperUI.transform.Find("Meowney/Content Text").GetComponent<Text>();
     
        StartGame();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void AddMove()
    {
        move++;
        UpdateUI();
    }

    public void RestartAfterFinishGame()
    {
        clearScene.SetActive(false);
        isRestart = true;
        StartGame();
    }
    public void RestartGame()
    {
        GameEndEventHandler();
        isRestart = true;
        StartGame();
    }
    internal void FinishGame()
    {
        CatPictureManager.showRibbon = false;
        if (CatSelector.isRandom)
        {
            CatSelector.GetRandomCat();
            GetACat(CatSelector.rescuedCatCategory, CatSelector.rescuedCatNumber);
            DataManager.Save();
        }

        clearScene.transform.Find("panel/AdForMeowney").gameObject.SetActive(true);
        clearScene.SetActive(true);
        GameEndEventHandler();
        GetMeowney();
    }

    public void GetACat(string category, int number)
    {
        Debug.Log(category);
        Debug.Log(number);

        switch (category)
        {
            case "Common":
                if (!DataManager.Instance.rescuedCommonCats.Contains(number))
                {
                    CatPictureManager.showRibbon = true;
                    DataManager.Instance.rescuedCommonCats.Add(number);
                }
                break;

            case "Rare":
                if (!DataManager.Instance.rescuedRareCats.Contains(number))
                {
                    CatPictureManager.showRibbon = true;
                    DataManager.Instance.rescuedRareCats.Add(number);
                }

                if (!DataManager.Instance.unlockedRareCats.Contains(number))
                {
                    DataManager.Instance.unlockedRareCats.Add(number);
                }
                break;

            case "Special":
                if (!DataManager.Instance.rescuedSpecialCats.Contains(number))
                {
                    CatPictureManager.showRibbon = true;
                    DataManager.Instance.rescuedSpecialCats.Add(number);
                }

                if (!DataManager.Instance.unlockedSpecialCats.Contains(number))
                {
                    DataManager.Instance.unlockedSpecialCats.Add(number);
                }
                break;
        }
        DataManager.Save();
    }

    internal void GetRewardMeowney()
    {
        clearScene.transform.Find("panel/AdForMeowney").gameObject.SetActive(false);
        int meowney = (stage / 10) + 1;
        DataManager.Instance.meowney += meowney * 3;
        StartCoroutine(CountUpMeowney(meowney));
    }

    IEnumerator CountUpMeowney(int meowney)
    {
        int goal = meowney * 4;
        while (meowney <= goal)
        {
            addMeowneyUI.GetComponent<AudioSource>().Play();
            addMeowneyUI.text = "+" + (meowney++).ToString();
            yield return new WaitForSeconds(0.2f);
        }
    }
    private void GetMeowney()
    {
        int meowney = (stage / 10) + 1;

        if(meowney >= 10)
        {
            meowney = 10;
        }

        DataManager.Instance.meowney += meowney;
        addMeowneyUI.text = "+" + meowney.ToString();
    }

    public void LoseMeowney(int meowney)
    {
        DataManager.Instance.meowney -= meowney;
        UpdateUI();
    }

    private void UpdateUI()
    {
        stageUI.text = (stage + 1).ToString();
        moveUI.text = move.ToString();
        MeowneyUI.text = DataManager.Instance.meowney.ToString();
    }

    public void StartNextLevel()
    {
        clearScene.SetActive(false);
        stage++;
        UpdateUI();

        if(stage+1 % 10 == 0)
            GameObject.FindGameObjectWithTag("AdManager").GetComponent<GoogleMobileAdsObject>().ShowInterstitial();
        
        StartGame();
    }
    public void StartGame()
    {
        CheckIfCanGetStageUnlockCat();
        move = 0;
        UpdateUI();
        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().LoadLevel(stage % LevelRepository.numberOfLevels);
        isRestart = false;
    }

    internal void CheckIfCanUnlockAdCat()
    {
        if (DataManager.Instance.seenAd == 1)
        {
            GetACat("Rare", 9);
        }
        if (DataManager.Instance.seenAd == 3)// 계란초밥냥
        {
            GetACat("Rare", 22);
        }
        if (DataManager.Instance.seenAd == 5)
        {
            GetACat("Rare", 1);
        }
        if (DataManager.Instance.seenAd == 10)
        {
            GetACat("Rare", 12);
        }
        if (DataManager.Instance.seenAd == 20) // 미믹냥
        {
            GetACat("Special", 9);
        }
    }
    private void CheckIfCanGetStageUnlockCat()
    {
        if(stage == 30 - 1) // 크로마
        {
            GetACat("Rare", 4);
        }
        else if (stage == 40 - 1) // 알파
        {
            GetACat("Rare", 5);
        }
        else if (stage == 50 - 1) // 세일러냥
        {
            GetACat("Rare", 18);
        }
        else if (stage == 70 - 1) // 산타냥
        {
            GetACat("Rare", 19);
        }
        else if (stage == 100 - 1) // 새우초밥냥
        {
            GetACat("Rare", 21);
        }
        else if (stage == 200 - 1) // 유니콘냥
        {
            GetACat("Rare", 29);
        }
        else if (stage == 300 - 1) // 호랑이
        {
            GetACat("Special", 0);
        }
    }

    public void SkipStage()
    {
        GameObject.FindGameObjectWithTag("AdManager").GetComponent<GoogleMobileAdsObject>().ShowInterstitial();
        GameEndEventHandler();
        StartNextLevel();
    }

    public void ShowRewardedAdForMeowney()
    {
        GameObject.FindGameObjectWithTag("AdManager").GetComponent<GoogleMobileAdsObject>().ShowRewardedAdForMeowney();
    }


}
