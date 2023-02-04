using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameConfigBase : MonoBehaviour
{
    public abstract GameConfigAssetBase Asset {get;}

    public float TimeLimit { get { return Asset.TimeLimit; } }
    public float TimeScale { get { return Asset.TimeScale; } }
}