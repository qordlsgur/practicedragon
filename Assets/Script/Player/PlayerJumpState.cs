using UnityEngine;

public class PlayerJumpState : State<Player>
{

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

        if (owner.IsGrounding)
            fsm.ChangeState(owner.IdleState);

        if (owner.IsFalling)
            fsm.ChangeState(owner.FallState);
    }

    public override void Exit()
    {

    }

    public override void OnAttackInput()
    {
        fsm.SaveStaet();
        fsm.ChangeState(owner.AttackState);
    }

    public override void OnJumpInput()
    {

    }
}
