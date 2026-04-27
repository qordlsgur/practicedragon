using System.Collections.Generic;
using UnityEngine;

public class Monster1 : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Run,
        Attack
    }

    public enum RunType
    {
        None,
        Patrol,
        Run
    }

    private FSM<Monster1> fsm;

    public MonsterIdleState IdleState => idle;
    private MonsterIdleState idle;
    public Monster1RunState RunState => run;
    private Monster1RunState run;
    public Monster1AttackState AattackState => attack;
    private Monster1AttackState attack;
    public Monster1DeathState DeathState => death;
    private Monster1DeathState death;

    private Vector2 TargetDistance;

    private Transform Player;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private Vector2 Size;

    public MonsterState currentState;
    public RunType currentRunType;

    private float moveSpeed = 10f;
    private float DetectRange = 4f;
    private float AttackRange = 2f;
    private float HeightRange = 3f;

    public bool IsGrounding => IsGround;
    private bool IsGround = false;

    public bool IsWallAhead => IsWall;
    private bool IsWall = false;
    private float rayDistance = 0.1f;

    public bool Dir = false;
    public float MoveDir = 0;

    public bool IsTargeting => IsTarget;
    private bool IsTarget = false;

    [SerializeField] private List<BoxCollider2D> HitCollider;
    private LayerMask GroundMask;

    private void Awake()
    {
        moveSpeed = 1f;
        MoveDir = 0;

        fsm = new FSM<Monster1>();
        idle = new MonsterIdleState(this, fsm);
        run = new Monster1RunState(this, fsm);
        attack = new Monster1AttackState(this, fsm);
        Player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        fsm.Init(idle);
        currentState = MonsterState.Idle;
        currentRunType = RunType.None;
        Size = spriteRenderer.bounds.size;
        GroundMask = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        fsm.fixedUpdate();
    }

    private void Update()
    {
        Dir = spriteRenderer.flipX;
        CheckGround();
        CheckWall();
        fsm.Update();
    }

    private void LateUpdate()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    public MonsterState Targeting()
    {
        TargetDistance = Player.position - transform.position;
        float distSP = TargetDistance.sqrMagnitude;

        if (distSP > DetectRange * DetectRange ||
            Mathf.Abs(TargetDistance.y) > HeightRange)
            return MonsterState.Idle;

        else if (distSP > AttackRange * AttackRange)
            return MonsterState.Run;

        else
            return MonsterState.Attack;
    }

    public void Move(float x)
    {
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
    }

    public void Stop()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public void Flip(float x)
    {
        if (x < 0)
            spriteRenderer.flipX = true;
        else if (x > 0)
            spriteRenderer.flipX = false;
    }

    public void CheckGround()
    {
        Vector2 center = HitCollider[0].bounds.center;
        float width = HitCollider[0].bounds.size.x;
        float height = HitCollider[0].bounds.size.y;

        float offsetX = (width / 2) * 0.8f;

        Vector2 currentFoot = Dir ? new Vector2(center.x - offsetX, center.y - height / 2)
                                    : new Vector2(center.x + offsetX, center.y - height / 2);

        Debug.DrawRay(currentFoot, Vector2.down * rayDistance, Color.green);

        IsGround = Physics2D.Raycast(currentFoot, Vector2.down, rayDistance, GroundMask);
    }

    public void CheckWall()
    {
        Vector2 center = HitCollider[0].bounds.center;
        float width = HitCollider[0].bounds.size.x / 2;
        Vector2 currentFront = Dir ? new Vector2(center.x - width, center.y)
                                    : new Vector2(center.x + width, center.y);

        Vector2 dirVec = Dir ? Vector2.right : Vector2.left;

        Debug.DrawRay(currentFront, dirVec * rayDistance, Color.blue);

        IsWall = Physics2D.Raycast(currentFront, dirVec, rayDistance, GroundMask);
    }

    public void IdelEnter()
    {
        anim.Play("Idle");
        anim.SetBool("Run", false);
        currentRunType = RunType.None;
        currentState = MonsterState.Idle;
    }

    public void RunEnter()
    {
        anim.SetBool("Run", true);
    }

    public void HitEnter()
    {

    }
}
