using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yabawee
{
    public class Hand : MonoBehaviour
    {
        RectTransform _rectTransform = null;
        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
    }
}
