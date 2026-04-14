using UnityEngine;
using UnityEngine.Timeline;

public class PlayerIdleState : State<Player>
{
    private Vector2 MoveInput;
    private Vector2 Movedelta;

    public PlayerIdleState(Player owner, FSM<Player> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
        owner.IdleEnter();
    }

    public override void FSMFixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x == 0)
            owner.Stop();
    }

    public override void FSMUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        owner.PlayerFlip(x);

        if (x != 0 && !owner.IsAttackinh)
            fsm.ChangeState(owner.RunState);

        if (!owner.IsJumpping && owner.IsFalling)
            fsm.ChangeState(owner.FallState);

    }

    public override void Exit()
    {
    }
}