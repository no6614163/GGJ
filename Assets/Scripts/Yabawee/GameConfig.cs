using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yabawee
{
    public class GameConfig : GameConfigBase
    {
        [SerializeField] GameConfigAsset[] assets;
        GameConfigAsset asset {
            get { return assets[GameSystem.Instance.GameLevel]; }
        }
        

        public float ShuffleDuration;
        public int RoundCount { get { return asset.ShufflePerRound.Length; } }
        public float ShuffleInterval;
        public float RoundInterval;
        public float YMovement;
        public float CupSpacing;

        public override GameConfigAssetBase Asset => asset;

        public int CupCount { get { return asset.CupCount; } }
        public int[] ShufflePerRound { get { return asset.ShufflePerRound; } }
        public int[] MinItemMoveCount { get { return asset.MinItemMoveCount; } }
        public Vector2Int[] FalseShuffleRangePerRound { get { return asset.FalseShuffleRangePerRound; } }

    }
}
