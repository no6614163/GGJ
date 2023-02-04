﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : Singleton<GameSystem>
{
    public int Gold { get; private set; }
    public int HappyPoint { get; private set; }
    public int FoodPoint { get; private set; }

    public int Day { get; private set; }
    public int CurrentStage { get; private set; }
    public int[] ScoreArray { get; private set; }

    public int GameLevel { get; private set; }

    public List<ShopItem> ShopItems;


    void Awake()
    {
        CurrentStage = 1;
        ScoreArray = new int[Constants.MaxStageCount];
        // NOTE : csv 파일에서 테이블 불러와서 저장
        SpecLoader<ShopItem> specLoader = new SpecLoader<ShopItem>("Data/ItemData");
        ShopItems = specLoader.GetAllSpecList();
        DontDestroyOnLoad(this);
    }

    public void InitGameSystem()
    {
        CurrentStage = 1;
        ScoreArray = new int[Constants.MaxStageCount];



    }
    public void SetGold(int gold)
    {
        Gold += gold;
    }

    public void SetHappyPoint(int happyPoint)
    {
        HappyPoint += happyPoint;

    }
    public void SetFoodPoint(int foodPoint)
    {
        FoodPoint += foodPoint;
    }
    
    public void SetScore(int score)
    {
        ScoreArray[CurrentStage-1] = score;
    }

    public void SetNextDay()
    {
        Day++;
        // TODO : 해피포인트, 푸드포인트 -10씩 줄어들기?
        SetHappyPoint(-10);
        SetFoodPoint(-10);
    }

    public void SetNextStage()
    {
        CurrentStage++;
    }

    public void SetGameLevel(int level)
    {
        GameLevel= level;
    }

    /// <summary>
    /// SceneIdx : Define.cs 에서 Scene index 확인 및 projectsetting에 scene index 세팅해줘야됨.
    /// </summary>
    public void LoadScene(int sceneIdx)
    {
        SceneManager.LoadScene(sceneIdx);
    }



}
