using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerStateDashing : iState
{
    private PlayerStateMachine playerStateMachine;
    private CharacterController characterController;

    public static event Action DashStartedEvent;
    public static event Action<float> DashEndedEvent;

    [Header("Dash Info")]
    [SerializeField] private float forceToApply;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    private bool IsInCooldown { get { return Time.time <= nextTimeCanDash; } }
    private bool DashSuccessful = true;
    private float nextTimeCanDash = float.MinValue;
    private float timeForDashToEnd;
    private Vector3 directionToDash;

    public void UpdateComponents(PlayerStateMachine playerStateMachine,CharacterController characterController)
    {
        this.playerStateMachine = playerStateMachine;
        this.characterController = characterController;
    }

    public void Enter()
    {
        //Have to check for cooldown if on PC as player can spam dash button.
        //If on mobile, the dash button is disabled and gets re-enabled once dash cooldown ends.
        #if UNITY_STANDALONE
        if (IsInCooldown)
        {
            DashSuccessful = false;
            playerStateMachine.RevertState();
            return;
        }
        #endif

        //Set up Dash.
        DashStartedEvent?.Invoke();

        DashSuccessful = true;
        directionToDash = InputManager.MovementInput;
        playerStateMachine.myTransform.forward = directionToDash;

        timeForDashToEnd = Time.time + dashDuration;

        playerStateMachine.playerAnimationController.GoToDash();
    }

    public void Exit() 
    { 
        //If the dash was in cooldown, state immediately exits and hits this, hence the check for if they had just dashed.

        if(DashSuccessful) 
        {
            DashEndedEvent?.Invoke(dashCooldown);
            nextTimeCanDash = Time.time + dashCooldown;
        }
    }

    public void Tick()
    {
        //Dash Movement.
        characterController.SimpleMove(directionToDash * forceToApply);

        if (Time.time >= timeForDashToEnd)
            playerStateMachine.RevertState();
    }

}
