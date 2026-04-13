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
        owner.anim.SetBool("Run", false);
        owner.anim.SetBool("Jump", false);
        owner.anim.SetBool("Fall", false);
        owner.IsJump = false;
    }

    public override void FSMFixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x == 0)
        {
            owner.rb.linearVelocity = new Vector2(0, owner.rb.linearVelocity.y);
        }
    }

    public override void FSMUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x < 0)
            owner.spriteRenderer.flipX = true;
        else if (x > 0)
            owner.spriteRenderer.flipX = false;

        if (x != 0)
            fsm.ChangeState(owner.run);

        if (!owner.IsJump && owner.rb.linearVelocity.y < 0)
            fsm.ChangeState(owner.fall);


    }

    public override void Exit()
    {
    }
}