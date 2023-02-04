using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch
{
    public class CardBehaviour : MonoBehaviour
    {
        [SerializeField] Image contentImage;
        [SerializeField] ScaleAtHover frontAnim;
        [SerializeField] ScaleAtHover backAnim;

        Animator animator;
        LevelManager levelManager;
        public Sprite Sprite { get; private set; }
        bool _clickable = false;
        public bool Clickable { get { return _clickable; }
            set {
                _clickable = value;
                frontAnim.IsAnimAble = value;
                backAnim.IsAnimAble = value;
            } }

        [SerializeField] bool isDesiredFront = false;
        RectTransform rectTransform;
        public RectTransform RectTransform { get { return rectTransform; } }
        private void Awake()
        {
            animator = GetComponent<Animator>();
            rectTransform = GetComponent<RectTransform>();
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
            SoundManager.Instance.PlaySFXPitched("Hover", "GameCommon", 0.05f);
        }
        public void OnPointerExit()
        {
            if (!Clickable)
                return;
        }
        public void OnWrong()
        {
            animator.SetTrigger("Wrong");
        }
        public void OnCorrect()
        {
            animator.SetTrigger("Correct");
        }
        public void OnClick()
        {
            if (!Clickable)
                return;
            levelManager.OnCardClicked(this);
            SoundManager.Instance.PlaySFXPitched("Click", "GameCommon", 0.1f);
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
