using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;

public class Player : MonoBehaviour
{
    private FSM<Player> fsm;

    private State<Player> currentState;

    public PlayerIdleState IdleState => idle;
    private PlayerIdleState idle;
    public PlayerRunState RunState => run;
    private PlayerRunState run;
    public PlayerAttackState AttackState => attack;
    private PlayerAttackState attack;
    public PlayerJumpState JumpState => jump;
    private PlayerJumpState jump;
    public PlayerFallState FallState => fall;
    private PlayerFallState fall;

    [SerializeField] private List<BoxCollider2D> ChildCollider;

    private struct AttackColliderOffset
    {
        private float AttackoffsetX;
        private float AttackoffsetY;
        private Vector2 NewAttackCollideroffset;
    }

    private float moveSpeed = 10f;
    private float jumpForce = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D[] attackCollider;

    public bool IsAttackinh => IsAttack;
    private bool IsAttack = false;

    public bool IsJumpping => IsJump;
    private bool IsJump = false;

    public bool IsGrounding => IsGround;
    private bool IsGround = false;
    public bool IsFalling => rb.linearVelocity.y < -0.1f;

    private void Awake()
    {
        moveSpeed = 10f;
        jumpForce = 7.5f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackCollider = GetComponents<BoxCollider2D>();

        fsm = new FSM<Player>();
        idle = new PlayerIdleState(this, fsm);
        run = new PlayerRunState(this, fsm);
        attack = new PlayerAttackState(this, fsm);
        jump = new PlayerJumpState(this, fsm);
        fall = new PlayerFallState(this, fsm);
    }

    private void Start()
    {
        fsm.Init(idle);
        foreach (var child in ChildCollider)
            child.enabled = false;
    }

    private void FixedUpdate()
    {
        ChakGround();
        fsm.fixedUpdate();
    }

    private void Update()
    {
        PlayerAttack();
        fsm.Update();

        Debug.DrawRay(transform.position, Vector2.down, Color.red, 1.4f);
    }

    private void LateUpdate()
    {
    }

    public void ChakGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, Vector2.down,
            1.4f, LayerMask.GetMask("Ground"));

        IsGround = hit.collider != null;
    }

    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
        }

        if (Input.GetKeyDown(KeyCode.X))
            fsm.CurrentState.OnAttackInput();

        if (Input.GetKeyDown(KeyCode.C) && !IsJump && !IsFalling)
            fsm.CurrentState.OnJumpInput();
    }

    public void Move(float x)
    {
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public void Stop()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public void PlayerFlip(float x)
    {
        if (x < 0)
            spriteRenderer.flipX = true;
        else if (x > 0)
            spriteRenderer.flipX = false;
    }

    public void IdleEnter()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Jump", false);
        anim.SetBool("Fall", false);
        IsJump = false;
    }

    public void RunEnter()
    {
        anim.SetBool("Run", true);
    }

    public void JumpEnter()
    {
        anim.SetBool("Jump", true);
        IsJump = true;
    }

    public void FallEnter()
    {
        anim.SetBool("Fall", true);

        if (IsJump)
        {
            anim.SetBool("Jump", false);
            IsJump = false;
        }
    }

    public void NormalAttack1()
    {
        anim.SetBool("Attack1", true);
        IsAttack = true;
    }

    public void AttackCollider()
    {
        ChildCollider[0].enabled = true;
    }

    public void NormalAttack1End()
    {
        anim.SetBool("Attack1", false);
        IsAttack = false;
        ChildCollider[0].enabled = false;
    }

}
