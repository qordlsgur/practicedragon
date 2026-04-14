using UnityEngine;

public class PlayerAttackState : State<Player>
{
    public PlayerAttackState(Player owner, FSM<Player> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
        owner.NormalAttack1();
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
