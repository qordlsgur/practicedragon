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

        if (owner.IsFalling && owner.IsGround())
            fsm.ChangeState(owner.IdleState);

    }

    public override void Exit()
    {

    }

}
