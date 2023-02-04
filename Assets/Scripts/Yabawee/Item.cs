using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yabawee
{
    public class Item : MonoBehaviour
    {
        public RectTransform RectTransform { get; private set; }

        Animator animator;
        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            animator = GetComponent<Animator>();
        }
        public void Shake()
        {
            animator.SetTrigger("Shake");
        }
    }
}
