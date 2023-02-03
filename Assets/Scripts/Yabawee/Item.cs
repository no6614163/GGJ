using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yabawee
{
    public class Item : MonoBehaviour
    {
        public RectTransform RectTransform { get; private set; }
        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
    }
}
