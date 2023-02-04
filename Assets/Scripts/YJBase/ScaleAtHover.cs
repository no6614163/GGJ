using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleAtHover : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;
    public bool PlaySound = false;

    public bool IsAnimAble { get; set; } = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PointerEnter()
    {
        if (IsAnimAble)
        {
            animator.SetTrigger("PointerEnter");
            if (PlaySound)
                SoundManager.Instance.PlaySFXPitched("Hover", "GameCommon", 0.05f);
        }
    }
    public void PointerExit()
    {
        if (IsAnimAble)
            animator.SetTrigger("PointerExit");
    }
    public void Click()
    {
        if (IsAnimAble)
        {
            animator.SetTrigger("Click");
            if (PlaySound)
                SoundManager.Instance.PlaySFXPitched("Click", "GameCommon", 0.1f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsAnimAble)
        {
            animator.SetTrigger("Click");
            if (PlaySound)
                SoundManager.Instance.PlaySFXPitched("Click", "GameCommon", 0.1f);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsAnimAble)
        {
            animator.SetTrigger("PointerEnter");
            if (PlaySound)
                SoundManager.Instance.PlaySFXPitched("Hover", "GameCommon", 0.05f);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsAnimAble)
            animator.SetTrigger("PointerExit");
    }
}