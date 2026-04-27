using UnityEngine;

public class MonsterIdleState : State<Monster1>
{
    float idleTimer;
    float waitTime;
    float NextState = 0f;

    public MonsterIdleState(Monster1 owner, FSM<Monster1> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
        owner.IdelEnter();
        idleTimer = 0f;
        waitTime = Random.Range(0f, 3f);
        owner.MoveDir = NextState = Random.Range(0, 3) - 1;
    }

    public override void FSMFixedUpdate()
    {
        owner.Stop();
    }

    public override void FSMUpdate()
    {
        idleTimer += Time.deltaTime;

        var state = owner.Targeting();

        if (state == Monster1.MonsterState.Attack)
        {
            fsm.ChangeState(owner.AattackState);
            return;
        }

        if (state == Monster1.MonsterState.Run)
        {
            owner.currentRunType = Monster1.RunType.Run;
            fsm.ChangeState(owner.RunState);
            return;
        }

        if (idleTimer >= waitTime)
        {
            switch (NextState)
            {
                case -1:
                    owner.currentRunType = Monster1.RunType.Patrol;
                    fsm.ChangeState(owner.RunState);
                    break;

                case 0:
                    owner.currentRunType = Monster1.RunType.None;
                    NextMove();
                    break;

                case 1:
                    owner.currentRunType = Monster1.RunType.Patrol;
                    fsm.ChangeState(owner.RunState);
                    break;
            }
            return;
        }
    }

    public override void Exit()
    {

    }

    public override void OnAttackInput()
    {

    }

    public void NextMove()
    {
        idleTimer = 0f;
        waitTime = Random.Range(0f, 3f);
        owner.MoveDir = NextState = Random.Range(0, 3) - 1;
    }
}
