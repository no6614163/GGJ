using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumpJump
{
    public class ScrollSet : MonoBehaviour
    {
        [SerializeField] bool isBackground;
        [SerializeField] LevelManager levelManager;
        [SerializeField] RectTransform[] set1;
        [SerializeField] RectTransform[] set2;

        float width;
        private void Awake()
        {
            width = GetComponent<RectTransform>().rect.width;
            foreach (var rectT in set2)
            {
                    rectT.anchoredPosition += new Vector2(width , 0);
            }
        }

        private void Update()
        {
            float scrollSpeed = levelManager.Config.ScrollSpeed;
            if (isBackground)
                scrollSpeed *= levelManager.Config.BgScrollSlow;
            foreach (var rectT in set1)
            {
                rectT.anchoredPosition += new Vector2(-scrollSpeed, 0) * Time.deltaTime * levelManager.Config.TimeScale;
                if (rectT.anchoredPosition.x < -width)
                    rectT.anchoredPosition += new Vector2(width * 2, 0);
            }
            foreach (var rectT in set2)
            {
                rectT.anchoredPosition += new Vector2(-scrollSpeed, 0) * Time.deltaTime * levelManager.Config.TimeScale;
                if (rectT.anchoredPosition.x < -width)
                    rectT.anchoredPosition += new Vector2(width * 2, 0);
            }
        }
    }
}
