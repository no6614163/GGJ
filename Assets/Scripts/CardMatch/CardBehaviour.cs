using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch
{
    public class CardBehaviour : MonoBehaviour
    {
        [SerializeField] Image contentImage;

        Animator animator;
        LevelManager levelManager;
        public Sprite Sprite { get; private set; }
        public bool Clickable { get; set; } = false;

        bool isDesiredFront = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        public void Init(Sprite sprite, LevelManager levelManager)
        {
            Sprite = sprite;
            contentImage.sprite = sprite;
            this.levelManager = levelManager;
        }
        void Flip(bool isFront)
        {
            if (isFront)
            {
                animator.SetTrigger("FlipToFront");
                SoundManager.Instance.PlaySFXToggle("FlipFront", "CardMatch");
            }
            else
            {
                animator.SetTrigger("FlipToBack");
                SoundManager.Instance.PlaySFXToggle("FlipBack", "CardMatch");
            }
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
            levelManager.OnCardClicked(this);
        }
        public void SetDesiredState(bool isFront)
        {
            isDesiredFront = isFront;
            if (isFront)
                Flip(isFront);
            else
                StartCoroutine(WaitAndFlipToBack());
        }
        IEnumerator WaitAndFlipToBack()
        {
            yield return new WaitForSeconds(levelManager.Config.CardShowDuration);
            if (!isDesiredFront)
                Flip(false);
        }
    }
}
