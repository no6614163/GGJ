using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using DamageNumbersPro;
public class EffectManager : Singleton<EffectManager>
{
    [SerializeField]
    private DamageNumberGUI m_Number;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SpawnNumber(Transform parent, int num, Vector2 pos)
    {
        var number = m_Number.Spawn(pos, num);
        number.SetAnchoredPosition(parent, pos);
    }



}