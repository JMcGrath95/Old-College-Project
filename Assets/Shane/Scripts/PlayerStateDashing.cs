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

    public static event Action<float> DashEndedEvent;

    [Header("Dash Info")]
    [SerializeField] private float forceToApply;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    //private bool CanDash { get { return !IsInCooldown && did } }
    private bool IsInCooldown { get { return Time.time <= nextTimeCanDash; } }
    private bool DidDash = true;
    private float timeForDashToEnd;
    private float nextTimeCanDash = float.MinValue;
    private Vector3 directionToDash;


    public void Enter()
    {
        if (IsInCooldown)
        {
            DidDash = false;
            playerStateMachine.RevertState();
            return;
        }

        DidDash = true;
        Debug.Log("entered dash state");
        directionToDash = InputManager.MovementInput;
        timeForDashToEnd = Time.time + dashDuration;

        playerStateMachine.playerAnimationController.GoToDash();
    }

    public void Exit() 
    { 
        if(DidDash)
        {
            DashEndedEvent?.Invoke(dashCooldown);
            nextTimeCanDash = Time.time + dashCooldown;
        }
    }

    public void Tick()
    {
        characterController.SimpleMove(directionToDash * forceToApply);

        if (Time.time >= timeForDashToEnd)
            playerStateMachine.RevertState();
    }

    public void UpdateComponents(PlayerStateMachine playerStateMachine,CharacterController characterController)
    {
        this.playerStateMachine = playerStateMachine;
        this.characterController = characterController;
    }
}
