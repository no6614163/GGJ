using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumpJump
{
    [CreateAssetMenu(fileName = "New JumpJump Config", menuName = "Configs/JumpJump")]
    public class GameConfigAsset : GameConfigAssetBase
    {
        public Sprite[] Characters;
        public Vector2 ObstacleDistanceRange;
    }
}