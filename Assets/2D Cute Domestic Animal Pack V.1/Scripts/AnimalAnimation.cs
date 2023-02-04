using System;
using UnityEngine;

public class AnimalAnimation : CharacterAnimation
{
    public Acting CurrentAct;
    void Awake()
    {
        CurrentAct = Acting.Jump;
    }

    public void SetAnim(Acting act)
    {
        SetAnimation(act);
        CurrentAct = act;
    }

    protected override void SetAnimation(Acting acting)
    {
        if (IsDead)
        {
            return;
        }

        Acting = acting;
        switch (acting)
        {
            case Acting.Eat:
                Animator.Play("eat");
                break;
            case Acting.Eat2:
                Animator.Play("eat4boar");
                break;
            case Acting.Idle:
                Animator.Play("idle");
                break;
            case Acting.Jump:
                Animator.Play("jump");
                break;
            case Acting.Sleep:
                Animator.Play("sleep");
                break;
            case Acting.Walk:
                Animator.Play("walk");
                break;
            case Acting.Idle2:
                Animator.Play("idle2");
                break;
        }
    }

}
