using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : BaseAnimationController
{
    private const string idleAnimation = "rig_Idle";
    private const string walkingAnimation = "rig_Walking";
    private const string meleeAttackAnimation = "rig_Attack Action";

    public override void Awake() => base.Awake();

    public void GoToIdle() => ChangeAnimationState(idleAnimation);
    public void GoToWalking() => ChangeAnimationState(walkingAnimation);
    public void GoToAttacking() => ChangeAnimationState(meleeAttackAnimation);

}
