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
    Text stageUI, moveUI;
    public static bool isRestart;

    public delegate void GameEndHandler();
    public static event GameEndHandler GameEndEventHandler;

    private void Start()
    {
        DataManager.Load();
        upperUI = GameObject.FindGameObjectWithTag("UpperUI");
        stageUI = upperUI.transform.Find("Stage/Content Text").GetComponent<Text>();
        moveUI = upperUI.transform.Find("Moves/Content Text").GetComponent<Text>();
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
        clearScene.SetActive(true);
        GameEndEventHandler();
        switch (CatSelector.catCategory)
        {
            case "Common":
                DataManager.Instance.rescuedCommonCats.Add(CatSelector.catNumber);
                break;

            case "Rare":
                DataManager.Instance.rescuedRareCats.Add(CatSelector.catNumber);
                break;

            case "Special":
                DataManager.Instance.rescuedSpecialCats.Add(CatSelector.catNumber);
                break;
        }
        DataManager.Save();
    }

    private void UpdateUI()
    {
        stageUI.text = (stage + 1).ToString();
        moveUI.text = move.ToString();
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

    public void TwitterShare()
    {
        string descriptionParam = "YOUR AWESOME GAME MESSAGE!";
        Application.OpenURL(TWITTER_ADDRESS +
           "?text=" + WWW.EscapeURL(descriptionParam + "\n" + "Get the Game:\n" + appStoreLink));

    }
}
