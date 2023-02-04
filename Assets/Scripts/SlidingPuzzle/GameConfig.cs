using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlidingPuzzle
{
    public enum ShuffleCount { Two = 2, Three = 3, Four = 4}
    public enum EmptyPieceType { LeftUp, LeftDown, RightUp, RightDown}
    public class GameConfig : GameConfigBase
    {
        [SerializeField] GameConfigAsset[] assets;
        GameConfigAsset asset
        {
            get { return assets[GameSystem.Instance.GameLevel]; }
        }
        public float PieceMoveDuration;
        public Sprite[] Pictures;
        public EmptyPieceType[] EmptyPiecePos;
        public override GameConfigAssetBase Asset => asset;

        public int ShuffleCount { get { return asset.ShuffleCount; } }
        public ShuffleCount BoardSize { get { return asset.BoardSize; }
}

    }
}
