using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : Singleton<GameSystem>
{
    public int Gold { get; private set; }
    public int HappyPoint { get; private set; }
    public int FoodPoint { get; private set; }

    public int Day { get; private set; }
    public int[] ScoreArray { get; private set; }
    public int CurrentStage { get; private set; }

    void Awake()
    {
        CurrentStage = 1;
        ScoreArray = new int[Constants.MaxStageCount];
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

    /// <summary>
    /// 점수 등록 
    /// </summary>
    /// <param name="stage"> 몇번째 스테이지인지 </param>
    public void SetScore(int stage, int score)
    {
        ScoreArray[stage - 1] = score;
    }

    /// <summary>
    /// SceneIdx : Define.cs 에서 Scene index 확인 및 projectsetting에 scene index 세팅해줘야됨.
    /// </summary>
    public void LoadScene(int sceneIdx)
    {
        SceneManager.LoadScene(sceneIdx);
    }



}
