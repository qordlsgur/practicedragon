using UnityEngine;

public class PlayerFallState : State<Player>
{
    public PlayerFallState(Player owner, FSM<Player> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
        owner.FallEnter();
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

    }

    public override void Exit()
    {
        fsm.SaveStaet();
    }

    public override void OnAttackInput()
    {

    }

    public override void OnJumpInput()
    {
        if (!owner.IsGrounding)
            return;

        fsm.ChangeState(owner.JumpState);
    }
}
