using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerBaseState : IState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerBaseData playerData;
    protected Status curState;
    
    public PlayerBaseState(PlayerStateMachine playerstateMachine)
    {
        stateMachine = playerstateMachine;
        player = playerstateMachine.Player;
        playerData = playerstateMachine.Player.Data;
        curState = player.currentStat;
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
        stateMachine.DashCoolTime += Time.deltaTime;

        if (player.isHit)
        {
            player.InvokeEvent(player.OnHitEvent);
        }
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
        if (stateMachine.DashCoolTime > player.currentStat.dashCoolTime)
        {
            stateMachine.ChangeState(stateMachine.dashState);
        }
        else
            return;
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
        Rotate(movementDirection);
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        stateMachine.Player.Controller.Move(
            (movementDirection * movementSpeed) * Time.deltaTime
            );
    }

    protected Vector3 GetMovementDirection()  // x와 z축으로만 움직이도록 방향 설정
    {

        Vector3 forward = new Vector3(0, 0, 1);       // stateMachine.MainCameraTransform.forward;
        Vector3 right = new Vector3(1, 0, 0);      // stateMachine.MainCameraTransform.right;

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private void Rotate(Vector3 movementDirection)
    {
        if(movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.Player.transform.rotation = Quaternion.Slerp(stateMachine.Player.transform.rotation, targetRotation, 1f );
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer > 6 /*&& player.hitDelay > 0.5f*/)
    //    {
    //        player.HealthSystem.TakeDamage(10f);   
    //        stateMachine.ChangeState(stateMachine.hitState);
    //    }
    //}



    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    public void GetEXP(int exp)
    {
        playerData.EXP += exp;
        if(playerData.EXP >= playerData.maxEXP )
        {
            playerData.EXP = 0;
            playerData.LV += 1;
        }
    }
}
