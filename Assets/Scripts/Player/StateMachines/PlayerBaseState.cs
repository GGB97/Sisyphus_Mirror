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
        playerData = playerstateMachine.Player.Data;
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

    

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Move.ReadValue<Vector2>();
    }

    protected void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        stateMachine.Player.Controller.Move(
            (movementDirection * movementSpeed) * Time.deltaTime
            );
    }

    protected Vector3 GetMovementDirection()  // x와 z축으로만 움직이도록 방향 설정
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
