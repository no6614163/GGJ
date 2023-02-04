using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    public class GameConfig : GameConfigBase
    {
        [SerializeField] GameConfigAsset data;
        public Sprite[] CardImages;
        public int rowCount = 4;
        public float CardShowDuration;

        public override GameConfigAssetBase Asset => data;

        public int ColCount { get { return data.ColCount; } }
        public float InitialShowingTime { get { return data.InitialShowingTime; } }
    }
}
