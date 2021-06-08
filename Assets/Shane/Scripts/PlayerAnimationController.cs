using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : BaseAnimationController
{
    private const string idleAnimation = "rig_Idle";
    private const string walkingAnimation = "Walking";
    private const string meleeAttackAnimation = "rig_Attack Action";
    private const string dashAnimation = "Dash 1";


    public override void Awake() => base.Awake();

    public void GoToIdle() => ChangeAnimationState(idleAnimation);
    public void GoToWalking() => ChangeAnimationState(walkingAnimation);
    public void GoToAttacking() => ChangeAnimationState(meleeAttackAnimation);
    public void GoToDash() => ChangeAnimationState(dashAnimation);

}
