using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;

public class Player : MonoBehaviour
{
    private FSM<Player> fsm;

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
    private float jumpTimer;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private LayerMask GroundMask;

    [SerializeField] private List<BoxCollider2D> HitCollider;

    public bool IsAttackinh => IsAttack;
    private bool IsAttack = false;

    public bool IsJumpping => IsJump;
    private bool IsJump = false;

    public bool IsGrounding => IsGround;
    private bool IsGround = false;
    public bool IsFalling => rb.linearVelocity.y < -0.1f;

    private float rayDistance = 0.1f;

    private void Awake()
    {
        moveSpeed = 10f;
        jumpForce = 7.5f;
        jumpTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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

        GroundMask = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        fsm.fixedUpdate();
    }

    private void Update()
    {
        PlayerAttack();
        CheckGround();
        fsm.Update();

        if (jumpTimer > 0)
            jumpTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
    }

    public void CheckGround()
    {
        if (rb.linearVelocity.y > 0.1f || jumpTimer > 0f)
        {
            IsGround = false;
            return;
        }

        Vector2 center = HitCollider[0].bounds.center;
        float width = HitCollider[0].bounds.size.x;
        float height = HitCollider[0].bounds.size.y;

        float offsetX = (width / 2) * 0.8f;

        Vector2 leftFoot = new Vector2(center.x - offsetX, center.y - height / 2);
        Vector2 centerFoot = new Vector2(center.x, center.y - height / 2);
        Vector2 rightFoot = new Vector2(center.x + offsetX, center.y - height / 2);

        IsGround = CheckRay(leftFoot) || CheckRay(centerFoot) || CheckRay(rightFoot);
    }

    private bool CheckRay(Vector2 Ray)
    {
        RaycastHit2D hit = Physics2D.Raycast(Ray, Vector2.down, rayDistance, GroundMask);

        if (hit.collider == null)
            return false;

        if (hit.normal.y <= 0.7f)
            return false;

        if (hit.distance > 0.03f)
            return false;

        return true;
    }

    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
        }

        if (Input.GetKeyDown(KeyCode.X))
            fsm.CurrentState.OnAttackInput();


        if (Input.GetKeyDown(KeyCode.C))
        {
            jumpTimer = 0.3f;
            fsm.CurrentState.OnJumpInput();
        }
    }

    public void Move(float x)
    {
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
    }

    public void Jump()
    {
        if (!IsJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public void Stop()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public void JumpStop()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
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
        anim.SetBool("Run", false);
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
        fsm.ReturnState();
    }
}
