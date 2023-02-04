using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlidingPuzzle
{
    public enum ShuffleCount { Two = 2, Three = 3, Four = 4}
    public enum EmptyPieceType { LeftUp, LeftDown, RightUp, RightDown}
    public class GameConfig : MonoBehaviour
    {
        public Sprite[] Pictures;
        public EmptyPieceType[] EmptyPiecePos;
        public int ShuffleCount;
        public ShuffleCount BoardSize = SlidingPuzzle.ShuffleCount.Three;
        public float TimeLimit;
        public float PieceMoveDuration;
    }
}
