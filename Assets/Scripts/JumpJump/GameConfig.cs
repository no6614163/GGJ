using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumpJump
{
    public class GameConfig : MonoBehaviour
    {
        public Sprite[] Characters;

        public float ScrollSpeed;
        public float BgScrollSlow;
        public float TimeLimit;
        public float CharactersRandomness;
        public float JumpPower;
        public float Gravity;
        public Vector2 ObstacleDistanceRange;
    }
}
