using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JumpJump
{
    public class Character : MonoBehaviour
    {
        LevelManager levelManager;
        Image image;
        float groundHeight;

        float velocity;

        float jumpQueueTime = 0;
        [SerializeField] float jumpQueueDuration;
        bool isJumping;

        RectTransform rectTransform;

        public float Radius;
        public Vector2 Position { get { return rectTransform.anchoredPosition; } set { rectTransform.anchoredPosition = value; } }
        public void Init(LevelManager levelManager, float groundHeight, Sprite sprite)
        {
            this.levelManager = levelManager;
            this.groundHeight = groundHeight;
            image.sprite = sprite;
        }
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
        }

        private void Update()
        {
            if(jumpQueueTime > 0)
                jumpQueueTime -= Time.fixedDeltaTime;
            if (Position.y <= groundHeight && velocity <= 0)
            {
                velocity = 0f;
                Position = new Vector2(Position.x, groundHeight);
                isJumping = false;
                if (jumpQueueTime > 0)
                {
                    jumpQueueTime = 0;
                    Jump();
                }
            }
            else
            {
                velocity -= levelManager.Config.Gravity * Time.deltaTime * levelManager.Config.TimeScale;
                Position += new Vector2(0, velocity) * Time.deltaTime * levelManager.Config.TimeScale;
            }
        }
        public void Jump()
        {
            if(isJumping)
            {
                jumpQueueTime = jumpQueueDuration;
            }
            else
            {
                //TODO : 점프 효과
                velocity = levelManager.Config.JumpPower * Random.Range(1f - levelManager.Config.CharactersRandomness * 0.05f, 1.0f);
                isJumping = true;
                jumpQueueTime = 0f;
            }
        }
        public void OnCollision()
        {

        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + new Vector3(0, Radius), Radius);
        }
#endif
    }
}
