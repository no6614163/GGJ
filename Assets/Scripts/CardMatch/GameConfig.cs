using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    public class GameConfig : GameConfigBase
    {
        [SerializeField] GameConfigAsset[] assets;
        GameConfigAsset asset
        {
            get { return assets[GameSystem.Instance.GameLevel]; }
        }
        public Sprite[] CardImages;
        public int rowCount = 4;
        public float CardShowDuration;

        public override GameConfigAssetBase Asset => asset;

        public int ColCount { get { return asset.ColCount; } }
        public float InitialShowingTime { get { return asset.InitialShowingTime; } }
    }
}
