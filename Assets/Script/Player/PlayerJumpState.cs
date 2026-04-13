using UnityEngine;

public class PlayerJumpState : State<Player>
{
    private Vector2 move;

    public PlayerJumpState(Player owner, FSM<Player> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
        owner.anim.SetBool("Jump", true);
        owner.IsJump = true;
        owner.rb.linearVelocity = new Vector2(owner.rb.linearVelocity.x, owner.jumpForce);
    }

    public override void FSMFixedUpdate()
    {

    }

    public override void FSMUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        // 공중에서도 좌우 이동
        Vector2 move = new Vector2(x * owner.moveSpeed, owner.rb.linearVelocity.y);
        owner.rb.linearVelocity = move;

        // 방향
        if (x < 0)
            owner.spriteRenderer.flipX = true;
        else if (x > 0)
            owner.spriteRenderer.flipX = false;

        // 착지 체크
        if (owner.rb.linearVelocity.y <= 0)
            fsm.ChangeState(owner.fall);
    }

    public override void Exit()
    {

    }
}
