using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleAtHover : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;

    public bool IsAnimAble { get; set; } = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PointerEnter()
    {
        if (IsAnimAble)
            animator.SetTrigger("PointerEnter");
    }
    public void PointerExit()
    {
        if (IsAnimAble)
            animator.SetTrigger("PointerExit");
    }
    public void Click()
    {
        if (IsAnimAble)
            animator.SetTrigger("Click");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(IsAnimAble)
        animator.SetTrigger("Click");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsAnimAble)
            animator.SetTrigger("PointerEnter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsAnimAble)
            animator.SetTrigger("PointerExit");
    }
}