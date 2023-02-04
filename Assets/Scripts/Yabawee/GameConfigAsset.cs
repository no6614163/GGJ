using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yabawee
{
    [CreateAssetMenu(fileName = "New Yabawee Config", menuName = "Configs/Yabawee")]
    public class GameConfigAsset : GameConfigAssetBase
    {
        public int CupCount;
        public int[] ShufflePerRound;
        public int[] MinItemMoveCount;
        public Vector2Int[] FalseShuffleRangePerRound;
    }
}