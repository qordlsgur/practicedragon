using UnityEngine;

public class PlayerAttackState : State<Player>
{
    public PlayerAttackState(Player owner, FSM<Player> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
        owner.NormalAttack1();

        if (!owner.IsJumpping)
            owner.Stop();
    }

    public override void FSMFixedUpdate()
    {

    }

    public override void FSMUpdate()
    {

    }

    public override void Exit()
    {

    }

}
