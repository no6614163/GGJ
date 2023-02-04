using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SlidingPuzzle
{
    public class Piece : MonoBehaviour
    {
        [SerializeField] Image image;
        public Vector2Int PosInGrid { get; set; }

        bool clickable = true;
        Animator animator;
        public bool Clickable
        {
            get { return clickable; }
            set { clickable = value; hoverAnim.IsAnimAble = value; }
        }
        public int ID { get; private set; }
        public Vector2 Position { get { return rectTransform.anchoredPosition; } set { rectTransform.anchoredPosition = value; } }
        public Vector2 Size { get { return rectTransform.rect.size; } set { rectTransform.sizeDelta = value; } }
        RectTransform rectTransform;
        LevelManager levelManager;

        [SerializeField] ScaleAtHover hoverAnim;
        public void Init(LevelManager levelManager, Sprite sprite, int id)
        {
            image.sprite = sprite;
            ID = id;
            this.levelManager = levelManager;
        }
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            animator = GetComponent<Animator>();
        }
        public IEnumerator RemoveBorder(float duration)
        {
            float eTime = 0f;
            Vector2 originalBorder = image.rectTransform.offsetMin;
            while(eTime < duration)
            {
                image.rectTransform.offsetMin = Vector2.Lerp(originalBorder, Vector2.zero, eTime / duration);
                image.rectTransform.offsetMax = Vector2.Lerp(originalBorder, Vector2.zero, eTime / duration);
                yield return null;
                eTime += Time.deltaTime;
            }
            image.rectTransform.offsetMin = Vector2.zero;
            image.rectTransform.offsetMax = Vector2.zero;
        }
        public void OnPointerEnter()
        {
            if (!Clickable)
                return;
        }
        public void OnPointerExit()
        {
            if (!Clickable)
                return;
        }
        public void OnClick()
        {
            if (!Clickable)
                return;
            if (!levelManager.OnPieceClicked(this))
            {
                animator.SetTrigger("Wrong");
                SoundManager.Instance.PlaySFX("Wrong", "GameCommon");
            }
        }
    }
}
