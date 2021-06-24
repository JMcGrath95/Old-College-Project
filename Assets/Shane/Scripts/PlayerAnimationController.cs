using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : BaseAnimationController
{
    private const string idleAnimation = "Idle";
    private const string walkingAnimation = "Walking";
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
        private get { return currentAttackSpeedModifier; }
        set 
        { 
            currentAttackSpeedModifier = Mathf.Clamp(currentAttackSpeedModifier, minAttackSpeedModifier, maxAttackSpeedModifier);
            animator.SetFloat("AttackSpeedMultiplier", currentAttackSpeedModifier);
        } 
    }

    public override void Awake() => base.Awake();
    private void Start()
    {
        attackAnimationQueue = new Queue<string>(attackAnimations);
        CurrentAttackSpeedModifier = 1f;
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
    //public void IncreaseAttackMultipler(float amount)
    //{
    //    CurrentAttackSpeedModifier += amount;
    //    animator.SetFloat("AttackSpeedMultiplier", CurrentAttackSpeedModifier);
    //}
    //public void DecreaseAttackMultipler(float amount)
    //{
    //    CurrentAttackSpeedModifier -= amount;
    //    animator.SetFloat("AttackSpeedMultiplier", CurrentAttackSpeedModifier);      
    //}

}
