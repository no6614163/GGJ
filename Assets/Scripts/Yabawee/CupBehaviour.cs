using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yabawee
{
    public class CupBehaviour : MonoBehaviour
    {
        [SerializeField] RectTransform handPosition;
        public RectTransform RectTransform { get; private set; }
        public bool Clickable { get; set; }
        public int ID { get; private set; }
        public Vector2 HandPosition { get { return handPosition.anchoredPosition + RectTransform.anchoredPosition; } }

        LevelManager levelManager;
        Hand hand = null;
        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
        public void Init(LevelManager levelManager, int id)
        {
            this.levelManager = levelManager;
            this.ID = id;
        }
        private void Update()
        {
            if (hand != null)
            {

                hand.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
                hand.RectTransform.anchoredPosition = HandPosition;
            }
        }
        public void SetGrabbed(Hand hand)
        {
            this.hand = hand;
        }
        public Hand SetRelease()
        {
            Hand hand = this.hand;
            hand.transform.SetAsLastSibling();
            this.hand = null;
            return hand;
        }

        
        public void OnPointerEnter()
        {
            if (!Clickable)
                return;
            Debug.Log("OnPointerEnter");
        }
        public void OnPointerExit()
        {
            if (!Clickable)
                return;
            Debug.Log("OnPointerExit");
        }
        public void OnClick()
        {
            if (!Clickable)
                return;
            Debug.Log("OnClick");
            levelManager.OnCupClicked(this);
        }
    }
}
