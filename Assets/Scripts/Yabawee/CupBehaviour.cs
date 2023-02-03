using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yabawee
{
    public class CupBehaviour : MonoBehaviour
    {
        [SerializeField] RectTransform handPosition;
        public RectTransform RectTransform { get; private set; }
        public Vector2 HandPosition { get { return handPosition.anchoredPosition + RectTransform.anchoredPosition; } }

        Hand hand;
        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
        public void SetGrabbed(Hand hand)
        {
            this.hand = hand;
            hand.transform.SetParent(transform);
        }
        public Hand SetRelease()
        {
            Hand hand = this.hand;
            hand.transform.SetParent(transform.parent);
            this.hand = null;
            return hand;
        }
    }
}
