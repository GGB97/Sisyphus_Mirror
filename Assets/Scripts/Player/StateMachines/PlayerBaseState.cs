using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerBaseData playerData;
    
    public PlayerBaseState(PlayerStateMachine playerstateMachine)
    {
        stateMachine = playerstateMachine;
        playerData = playerstateMachine.Player.Data.playerBaseStat;
    }

    public virtual void Enter()
    {
        AddInputActionCallback();
    }

    public virtual void Exit()
    {
        RemoveInputActionCallback();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        Move();
    }


    protected virtual void AddInputActionCallback()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.canceled += OnMoveCanceled;
        input.PlayerActions.Dash.started += OnDashStarted;
    }

    protected virtual void RemoveInputActionCallback()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.canceled -= OnMoveCanceled;
        input.PlayerActions.Dash.started -= OnDashStarted;
    }

    protected virtual void OnDashStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnMove()
    {
        stateMachine.ChangeState(stateMachine.walkState);
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        float movementSpeed = playerData.moveSpeed;
        stateMachine.Player.Controller.Move(
            (movementDirection * movementSpeed) * Time.deltaTime
            );
    }

    private Vector3 GetMovementDirection()
    {

        Vector3 forward = stateMachine.PlayerTransform.forward;
        Vector3 right = stateMachine.PlayerTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    //private void Move(Vector3 movementDirection)
    //{
    //    float movementSpeed = playerData.moveSpeed;
    //    stateMachine.Player.Controller.Move(
    //        (movementDirection * movementSpeed) * Time.deltaTime
    //        );
    //}

    //protected void StartAnimation(int animationHash)
    //{
    //    stateMachine.Player.Animator.SetBool(animationHash, true);
    //}

    //protected void StopAnimation(int animationHash)
    //{
    //    stateMachine.Player.Animator.SetBool(animationHash, false);
    //}
}
