using UnityEngine;
using static Monster1;

public class Monster1RunState : State<Monster1>
{
    float RunTimer = 0f;
    float ResetTimer = 0f;
    public Monster1RunState(Monster1 owner, FSM<Monster1> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
        owner.RunEnter();
        RunTimer = 0;
        ResetTimer = Random.Range(1f, 3f);
    }

    public override void FSMFixedUpdate()
    {
        owner.Flip(owner.MoveDir);
        owner.Move(owner.MoveDir);
    }

    public override void FSMUpdate()
    {
        RunTimer += Time.deltaTime;

        var runType = owner.currentRunType;
        var state = owner.Targeting();

        if(state == Monster1.MonsterState.Attack)
        {

        }
        else if(state == Monster1.MonsterState.Run)
        {
            if (runType == Monster1.RunType.Patrol)
            {
                if (!owner.IsGrounding)
                {
                    owner.MoveDir *= -1f;
                }
                if (RunTimer >= ResetTimer)
                {
                    fsm.ChangeState(owner.IdleState);
                }
            }
            else if (runType == Monster1.RunType.Run)
            {

            }
        }
        else
            fsm.ChangeState(owner.IdleState);
    }

    public override void Exit()
    {

    }

    public override void OnAttackInput()
    {

    }

}
