using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateDashing : iState
{
    private PlayerStateMachine playerStateMachine;

    private CharacterController characterController;

    [Header("Dash Info")]
    [SerializeField] private float forceToApply;
    private Vector3 directionToDash;
    //Testing - read duration from animator?
    [SerializeField] private float dashDuration;
    private float timeForDashToEnd;

    //IFrames?.


    public void Enter()
    {
        Debug.Log("entered dash state");
        directionToDash = InputManager.MovementInput;
        timeForDashToEnd = Time.time + dashDuration;


        //playerStateMachine.playerAnimationController.GoToDash();
    }

    public void Exit() { }

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
