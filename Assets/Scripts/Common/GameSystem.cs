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
    public int CurrentStage { get; private set; }

    public int GameLevel { get; private set; }
    public SceneType CurrentSceneType { get; private set; }

    public List<ShopItem> ShopItems;
    // TODO : 각 이벤트들 넣어줘야됨.
    public Queue<int> EventQueue;

    public List<GameObject> AnimalPrefabList;


    void Awake()
    {
        Init();
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += LobbySceneLoaded;
        SpecLoader<ShopItem> specLoader = new SpecLoader<ShopItem>("Data/ItemData");
        ShopItems = specLoader.GetAllSpecList();
        AnimalPrefabList = new List<GameObject>();
    }

    void Init()
    {
        CurrentStage = 1;
        HappyPoint = 100;
        FoodPoint = 100;
        Gold = 10000;
        // NOTE : csv 파일에서 테이블 불러와서 저장
        CurrentSceneType = SceneType.Start;
        AnimalPrefabList.Clear();
    }


    public void InitGameSystem()
    {
        CurrentStage = 1;
    }
    public void SetGold(int gold)
    {
        Gold += gold;
    }

    public void SetHappyPoint(int happyPoint)
    {
        HappyPoint = happyPoint + HappyPoint > 100 ? 100 : happyPoint + HappyPoint;
        

    }
    public void SetFoodPoint(int foodPoint)
    {
        FoodPoint = foodPoint + FoodPoint > 100 ? 100 : foodPoint + FoodPoint;
    }
    
    public void AddAnimal(AnimalType type)
    {
        // TODO : instantiate animal
        var go = Resources.Load<GameObject>("Prefabs/Animals/" + type.ToString());
        var animal = Instantiate(go);
        var pos = Random.insideUnitCircle.normalized * 5;
        animal.transform.position = pos;
        AnimalPrefabList.Add(go);
    }

    public void SetNextDay()
    {
        Day++;
        // 해피포인트, 푸드포인트 -30씩 줄어들기
        SetHappyPoint(-30);
        SetFoodPoint(-30);
    }

    public void PlayQueueEvent()
    {
        while (EventQueue.Count != 0)
        {
            if(EventQueue.TryDequeue(out int result))
            {
                // TODO : 시퀀스로 넣든 큐로 넣든 해줘야됨. 
                //result
            }
        }
    }

    void LobbySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == (int)SceneType.Lobby)
        {
            // 로비씬일 경우 만복도와 해피포인트가 0이하인지 체크 해서 게임오버 팝업
            if(HappyPoint <= 0 || FoodPoint <= 0)
            {
                InitGameSystem();
                UI_Manager.Instance.ShowPopupUI<UI_GameFailedPopup>();
            }
            else if(AnimalPrefabList.Count > 0)
            {
                for (int i = 0; i < AnimalPrefabList.Count; i++)
                {
                    var animal = Instantiate(AnimalPrefabList[i]);
                    var pos = Random.insideUnitCircle.normalized * 5;
                    animal.transform.position = pos;
                }
            }
        }
        else if(scene.buildIndex == (int)SceneType.Start)
        {
            Init();
        }
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
