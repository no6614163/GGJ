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

        public bool Clickable { get; set; } = true;
        public int ID { get; private set; }
        public Vector2 Position { get { return rectTransform.anchoredPosition; } set { rectTransform.anchoredPosition = value; } }
        public Vector2 Size { get { return rectTransform.rect.size; } set { rectTransform.sizeDelta = value; } }
        RectTransform rectTransform;
        LevelManager levelManager;
        public void Init(LevelManager levelManager, Sprite sprite, int id)
        {
            image.sprite = sprite;
            ID = id;
            this.levelManager = levelManager;
        }
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
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
            if (!levelManager.OnPieceClicked(this))
                Debug.Log("누를수 없음");
        }
    }
}
