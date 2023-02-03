using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    public class GameConfig : MonoBehaviour
    {
        public Sprite[] CardImages;
        public int rowCount;
        public int colCount;
        public float TimeLimit;
        public float InitialShowingTime;
        public float CardShowDuration;
    }
}
