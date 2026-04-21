using UnityEngine;

public class PlayerAttack2State : State<Player>
{
    public PlayerAttack2State(Player owner, FSM<Player> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
    }

    public override void FSMFixedUpdate()
    {
        if (!owner.IsJumpping || !owner.IsFalling)
            owner.Stop();
    }

    public override void FSMUpdate()
    {

    }

    public override void Exit()
    {

    }
}
