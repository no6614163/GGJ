using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumpJump
{
    public class GameConfig : GameConfigBase
    {
        [SerializeField] GameConfigAsset asset;
        public float ScrollSpeed;
        public float BgScrollSlow;
        public float CharactersRandomness;
        public float JumpPower;
        public float Gravity;

        public override GameConfigAssetBase Asset => asset;

        public Sprite[] Characters { get { return asset.Characters; } }
        public Vector2 ObstacleDistanceRange { get { return asset.ObstacleDistanceRange; } }
    }
}
