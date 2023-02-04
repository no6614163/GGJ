using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameConfigAssetBase : ScriptableObject
{
    public float TimeScale = 1f;
    public float TimeLimit = 20f;
}