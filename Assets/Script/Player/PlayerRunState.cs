using UnityEngine;

public class PlayerRunState : State<Player>
{
    private Vector2 move;

    public PlayerRunState(Player owner, FSM<Player> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
        owner.RunEnter();
    }

    public override void FSMFixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x == 0)
            owner.Stop();
        else
            owner.Move(x);
    }

    public override void FSMUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        owner.PlayerFlip(x);

        if (x == 0)
            fsm.ChangeState(owner.IdleState);

        if (owner.IsFalling)
            fsm.ChangeState(owner.FallState);
    }

    public override void Exit()
    {
    }

    public override void OnAttackInput()
    {
        fsm.ChangeState(owner.AttackState);
    }

    public override void OnJumpInput()
    {
        fsm.ChangeState(owner.JumpState);
    }
}
