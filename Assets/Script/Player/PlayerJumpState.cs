using UnityEngine;

public class PlayerJumpState : State<Player>
{
    private Vector2 move;

    public PlayerJumpState(Player owner, FSM<Player> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
        owner.Jump();
        owner.JumpEnter();
    }

    public override void FSMFixedUpdate()
    {

    }

    public override void FSMUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        owner.Move(x);

        owner.PlayerFlip(x);

        if (!owner.IsFalling && owner.IsFalling)
            fsm.ChangeState(owner.FallState);

        if(owner.IsFalling)
            fsm.ChangeState(owner.IdleState);
    }

    public override void Exit()
    {

    }

    public override void OnAttackInput()
    {
        fsm.ChangeState(owner.AttackState);
    }
}
