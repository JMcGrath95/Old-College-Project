using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BaseAnimationController : MonoBehaviour
{
    private Animator animator;
    protected string currentAnimation;

    public virtual void Awake() => animator = GetComponent<Animator>();

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }
}
