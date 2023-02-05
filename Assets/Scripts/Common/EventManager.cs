using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class EventManager : Singleton<EventManager>
{

    public GameEvent GameEvent { get { return gameEvent; } }
    private readonly GameEvent gameEvent = new GameEvent();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}


public class GameEvent
{
    /// <summary>
    /// 아이템 구매한 경우
    /// </summary>
    public event Action<int> OnPurchaseRequest;
    public void InvokePurchaseItem(int gold)
    {
        OnPurchaseRequest?.Invoke(gold);
    }

}

