using UnityEngine;

public class Monster1 : MonoBehaviour
{
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
    private Animation anim;
    private Vector2 Size;


    private float moveSpeed = 10f;
    private float DetectRange = 4f;
    private float AttackRange = 2f;
    private float HeightRange = 3f;

    private void Start()
    {
        moveSpeed = 10f;

        Player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animation>();

        Size = spriteRenderer.bounds.size;
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        IsTarget();
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

    private void IsTarget()
    {
        TargetDistance = Player.position - transform.position;

        float distSP = TargetDistance.sqrMagnitude;

        if (distSP > DetectRange * DetectRange ||
            Mathf.Abs(TargetDistance.y) > HeightRange)
        {

            return;
        }

        if (distSP > AttackRange * AttackRange)
        {

            //fsm.ChangeState(At)
        }
        else if (distSP <= AttackRange * AttackRange)
        {

        }
    }

    public void Move(float x)
    {
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
    }
}
