using UnityEngine;

public class Player : MonoBehaviour
{
    public FSM<Player> fsm;
    public PlayerIdleState idle;
    public PlayerRunState run;
    public PlayerAttackState attack;
    public PlayerJumpState jump;
    public PlayerFallState fall;

    public struct AttackColliderOffset
    {
        public float AttackoffsetX;
        public float AttackoffsetY;
        public Vector2 NewAttackCollideroffset;
    }

    public float moveSpeed = 10f;
    public float jumpForce = 5f;

    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer spriteRenderer;

    public BoxCollider2D[] attackCollider;
    [SerializeField] public AttackColliderOffset AttackOffset;
    public bool Attack = false;
    public bool IsJump = false;

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
        fsm.ChangeState(idle);
    }

    private void FixedUpdate()
    {
        fsm.fixedUpdate();

    }

    private void Update()
    {
        PlayerAttack();
        fsm.Update();

        if (IsJump)
            IsGround();

    }

    private void LateUpdate()
    {
    }

    public bool IsGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, Vector2.down,
            1.1f, LayerMask.GetMask("Ground"));

        return hit.collider != null;
    }

    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            fsm.ChangeState(attack);
        }
        if (Input.GetKeyDown(KeyCode.C) && !IsJump)
        {
            fsm.ChangeState(jump);
        }
    }
}
