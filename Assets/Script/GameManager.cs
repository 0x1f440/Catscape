using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private string appStoreLink = "http://www.YOUROWNAPPLINK.com";

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
        clearScene.transform.Find("panel/AdForMeowney").gameObject.SetActive(true);
        clearScene.SetActive(true);
        GameEndEventHandler();
        GetMeowney();
        switch (CatSelector.catCategory)
        {
            case "Common":
                if (!DataManager.Instance.rescuedCommonCats.Contains(CatSelector.catNumber))
                {
                    DataManager.Instance.rescuedCommonCats.Add(CatSelector.catNumber);
                }
                break;

            case "Rare":
                if (!DataManager.Instance.rescuedRareCats.Contains(CatSelector.catNumber))
                {
                    DataManager.Instance.rescuedRareCats.Add(CatSelector.catNumber);
                }

                if (!DataManager.Instance.unlockedRareCats.Contains(CatSelector.catNumber))
                {
                    DataManager.Instance.unlockedRareCats.Add(CatSelector.catNumber);
                }
                break;

            case "Special":
                if (!DataManager.Instance.rescuedSpecialCats.Contains(CatSelector.catNumber))
                {
                    DataManager.Instance.rescuedSpecialCats.Add(CatSelector.catNumber);
                }

                if (!DataManager.Instance.unlockedSpecialCats.Contains(CatSelector.catNumber))
                {
                    DataManager.Instance.unlockedSpecialCats.Add(CatSelector.catNumber);
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
        DataManager.Instance.meowney += meowney;
        addMeowneyUI.text = "+" + meowney.ToString();
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
        StartGame();
    }
    public void StartGame()
    {
        move = 0;
        UpdateUI();
        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().LoadLevel(stage % LevelRepository.numberOfLevels);
        isRestart = false;
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

    public void TwitterShare()
    {
        string descriptionParam = "YOUR AWESOME GAME MESSAGE!";
        Application.OpenURL(TWITTER_ADDRESS +
           "?text=" + WWW.EscapeURL(descriptionParam + "\n" + "Get the Game:\n" + appStoreLink));

    }
}
