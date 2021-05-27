using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;

    private string currentAnimation;
    private const string idleAnimation = "rig|Idle";
    private const string walkingAnimation = "rig|Walking";
    private const string meleeAttackAnimation = "rig|Attack Action 1";
    private float meleeAttackAnimationTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case meleeAttackAnimation:
                    meleeAttackAnimationTime = clip.length;
                    break;

            }
        }
    }


    //Change animation.
    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    public void GoToIdle() => ChangeAnimationState(idleAnimation);
    public void GoToWalking() => ChangeAnimationState(walkingAnimation);
    public void GoToAttacking(Action callAtEndOfAnimation)
    {
        ChangeAnimationState(meleeAttackAnimation);

        CallDelegateAfterDelay(callAtEndOfAnimation, meleeAttackAnimationTime);
    }

    private void CallDelegateAfterDelay(Action action,float delay)
    {
        StartCoroutine(CallDelegateAfterDelayCoroutine(action, delay));
    }
    private IEnumerator CallDelegateAfterDelayCoroutine(Action action,float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
