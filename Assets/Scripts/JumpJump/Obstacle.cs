using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JumpJump
{
    public class Obstacle : MonoBehaviour
    {
        RectTransform rectTransform;
        public float Radius;
        [SerializeField] Sprite[] sprites;
        public Vector2 Position { get { return rectTransform.anchoredPosition; } set { rectTransform.anchoredPosition = value; } }
        public Vector2 Size { get { return rectTransform.rect.size; } }
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            GetComponent<Image>().sprite = HappyUtils.Random.RandomElement(sprites);
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + new Vector3(0,Radius), Radius);
        }
#endif
    }
}
