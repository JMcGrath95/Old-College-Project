using System;
using System.Collections.Generic;
using UnityEngine;

//Attack speed modifier needs to be reworked

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : BaseAnimationController
{
    public static event Action<float> AttackSpeedModifierChangedEvent;

    private const string idleAnimation = "Idle";
    private const string walkingAnimation = "Walking_with_Lantern 0";
    private const string meleeAttackAnimation = "Hit Action 2";
    private const string dashAnimation = "Dashing Action 001";

    private readonly string[] attackAnimations = new string[3] { "Hit Action 2", "Hit Action 3", "Hit Action 4" };
    private Queue<string> attackAnimationQueue;

    [Header("Attack Speed Modifying")]
    [SerializeField] private float currentAttackSpeedModifier = 1f;
    [SerializeField] private float minAttackSpeedModifier;
    [SerializeField] private float maxAttackSpeedModifier;

    public float CurrentAttackSpeedModifier 
    { 
        get { return currentAttackSpeedModifier; }
        set 
        {
            float oldAttackSpeedModifier = currentAttackSpeedModifier;

            currentAttackSpeedModifier = value;
            currentAttackSpeedModifier = Mathf.Clamp(currentAttackSpeedModifier, minAttackSpeedModifier, maxAttackSpeedModifier);
            animator.SetFloat("AttackSpeedMultiplier", currentAttackSpeedModifier);

            //Find how much percentage the attack speed modifier changed.
            float increase = currentAttackSpeedModifier - oldAttackSpeedModifier;
            float percentageChange = increase / oldAttackSpeedModifier * 100;

            //Change melee cooldown in player attack.
            AttackSpeedModifierChangedEvent(percentageChange);
        } 
    }

    public override void Awake() => base.Awake();
    private void Start()
    {
        attackAnimationQueue = new Queue<string>(attackAnimations);
    }

    public void GoToIdle() => ChangeAnimationState(idleAnimation);
    public void GoToWalking() => ChangeAnimationState(walkingAnimation);
    public void GoToAttacking() => ChangeAnimationState(meleeAttackAnimation);
    public void GoToNextAttack()
    {
        string animationToPlay = attackAnimationQueue.Dequeue();
        ChangeAnimationState(animationToPlay);
        attackAnimationQueue.Enqueue(animationToPlay);       
    }

    public void GoToDash() => ChangeAnimationState(dashAnimation);
}
