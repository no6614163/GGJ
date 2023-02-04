using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yabawee
{
    public class GameConfig : MonoBehaviour
    {
        public int RoundCount;
        public int CupCount;
        public int [] ShufflePerRound;
        public int [] MinItemMoveCount;
        public Vector2Int [] FalseShuffleRangePerRound;
        public float ShuffleDuration;

        public float ShuffleInterval;
        public float RoundInterval;
        public float YMovement;
        public float CupSpacing;
    }
}
