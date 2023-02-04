using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class LevelManagerBase : MonoBehaviour
{
    private GameConfigBase _config;


    private float elapsedTime = 0f;
    private Coroutine timerCoroutine;
    private UI_Game gameUI;
    protected UI_Game GameUI { get { return gameUI; } }
    protected virtual void Awake()
    {
        //TODO : config 변경
        //--
        _config = GetComponent<GameConfigBase>();
        gameUI = UI_Manager.Instance.ShowSceneUI<UI_Game>();
    }

    protected abstract void InitGame();

    IEnumerator TickCoroutine()
    {
        elapsedTime = 0f;
        while(elapsedTime < _config.TimeLimit)
        {
            elapsedTime += Time.deltaTime;
            gameUI.SetTimeSlider(1 - elapsedTime / _config.TimeLimit);
            yield return null;
        }
        OnTimerEnd();
    }
    public void StartTimer()
    {
        timerCoroutine = StartCoroutine(TickCoroutine());
    }
    public void StopTimer()
    {
        if(timerCoroutine != null)
            StopCoroutine(timerCoroutine);
    }
    public virtual void OnTimerEnd()
    { }

    public void ShowResult(bool isWin)
    {
        if( isWin)
        {
            if(GameSystem.Instance.CurrentStage == Constants.MaxStageCount)
                UI_Manager.Instance.ShowPopupUI<UI_SuccessPopup>();
            else
                UI_Manager.Instance.ShowPopupUI<UI_ClearPopup>();
        }
        else
        {
            UI_Manager.Instance.ShowPopupUI<UI_FailedPopup>();
        }
    }

    
}