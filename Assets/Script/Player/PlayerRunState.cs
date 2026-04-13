using UnityEngine;

public class PlayerRunState : State<Player>
{
    private Vector2 move;

    public PlayerRunState(Player owner, FSM<Player> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
        owner.anim.SetBool("Run", true);
    }

    public override void FSMFixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x == 0)
        {
            owner.rb.linearVelocity = new Vector2(0, owner.rb.linearVelocity.y);
        }
        else
        {
            owner.rb.linearVelocity = new Vector2(x * owner.moveSpeed, owner.rb.linearVelocity.y);
        }
    }

    public override void FSMUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x < 0)
            owner.spriteRenderer.flipX = true;
        else if (x > 0)
            owner.spriteRenderer.flipX = false;

        if (x == 0)
        {
            fsm.ChangeState(owner.idle);
        }
    }

    public override void Exit()
    {
    }
}
