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

    private float currentAttackSpeedModifier = 1f;

    public override void Awake() => base.Awake();
    private void Start() => attackAnimationQueue = new Queue<string>(attackAnimations);

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
    public void IncreaseAttackMultipler(float amount)
    {
        currentAttackSpeedModifier += amount;
        animator.SetFloat("AttackSpeedMultiplier", currentAttackSpeedModifier);
    }
    public void DecreaseAttackMultipler(float amount)
    {
        currentAttackSpeedModifier -= amount;
        animator.SetFloat("AttackSpeedMultiplier", currentAttackSpeedModifier);      
    }

}
