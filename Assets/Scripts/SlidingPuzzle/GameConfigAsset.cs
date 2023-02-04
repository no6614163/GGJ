using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlidingPuzzle
{
    [CreateAssetMenu(fileName = "New SlidingPuzzle Config", menuName = "Configs/SlidingPuzzle")]
    public class GameConfigAsset : GameConfigAssetBase
    {
        public int ShuffleCount;
        public ShuffleCount BoardSize = SlidingPuzzle.ShuffleCount.Three;
    }
}