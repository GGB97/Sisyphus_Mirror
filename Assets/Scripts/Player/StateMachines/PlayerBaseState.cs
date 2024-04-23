using UnityEngine;

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
        if(stateMachine.DashCoolTime < player.currentStat.dashCoolTime)
        {
            stateMachine.DashCoolTime += Time.deltaTime;
        }

        if (player.isHit)
        {
            player.InvokeEvent(player.OnHitEvent);
            player.isHit = false;
        }
        if (player.currentStat.health <= 0)
        {
            player.ChangeDieState();
        }
    }


    protected virtual void AddInputActionCallback()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.canceled += OnMoveCanceled;
        input.PlayerActions.Dash.started += OnDashStarted;
        input.PlayerActions.ESC.started += ESCstarted;
    }


    protected virtual void RemoveInputActionCallback()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.canceled -= OnMoveCanceled;
        input.PlayerActions.Dash.started -= OnDashStarted;
        input.PlayerActions.ESC.started -= ESCstarted;
    }

    private void ESCstarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        GameManager.Instance.OpenMenu();
    }

    protected virtual void OnDashStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

        if (stateMachine.DashCoolTime > player.currentStat.dashCoolTime)
        {
            stateMachine.ChangeState(stateMachine.dashState);
            SkillCoolTimeUI.Instance.TestSkill(stateMachine.DashCoolTime, player.currentStat.dashCoolTime);
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
        // TODO : curState의 이동 속도 반영하기(아이템 디메리트)
        float movementSpeed = curState.moveSpeed * stateMachine.MovementSpeedModifier;
        stateMachine.Player.Controller.Move(
            (movementDirection * movementSpeed) * Time.deltaTime
            );
    }

    protected Vector3 GetMovementDirection()  // x와 z축으로만 움직이도록 방향 설정
    {

        Vector3 forward = stateMachine.CameraTransform.forward;
        Vector3 right = stateMachine.CameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.Player.transform.rotation = Quaternion.Slerp(stateMachine.Player.transform.rotation, targetRotation, 1f);
        }
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

}
