using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    [CreateAssetMenu(fileName ="New CardMatch Config", menuName = "Configs/CardMatch")]
    public class GameConfigAsset : GameConfigAssetBase
    {
        public int ColCount;
        public float InitialShowingTime;
    }
}