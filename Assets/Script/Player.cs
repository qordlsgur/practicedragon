using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    private struct AttackColliderOffset
    {
        public float AttackoffsetX;
        public float AttackoffsetY;
        public Vector2 NewAttackCollideroffset;
    }

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private bool MoveAnim = false;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private BoxCollider2D[] attackCollider;
    [SerializeField] private AttackColliderOffset AttackOffset;
    private bool Attack = false;


    [SerializeField] private Vector2 MoveInput;
    private Vector2 Movedelta;

    private void Awake()
    {
        moveSpeed = 10f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackCollider = GetComponents<BoxCollider2D>();
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movedelta);
    }

    private void Update()
    {
        PlayerAttack();
        PlayerMove();
    }

    private void LateUpdate()
    {
    }

    private void PlayerMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        MoveInput = new Vector2(x, y);

        if (!Attack)
            Movedelta = MoveInput.normalized * moveSpeed * Time.deltaTime;

        if (x != 0 || y != 0)
            MoveAnim = true;
        else
            MoveAnim = false;


        if (MoveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (MoveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        AttackOffset.AttackoffsetX = attackCollider[0].offset.x;
        AttackOffset.AttackoffsetY = attackCollider[0].offset.y;
        AttackOffset.NewAttackCollideroffset.x = spriteRenderer.flipX ? -AttackOffset.AttackoffsetX : AttackOffset.AttackoffsetX;
        AttackOffset.NewAttackCollideroffset.y = AttackOffset.AttackoffsetY;
        attackCollider[0].offset = AttackOffset.NewAttackCollideroffset;

        anim.SetBool("Run", MoveAnim);
    }

    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z");
            Attack = true;
        }
        if (Input.GetKeyDown(KeyCode.X))
            Debug.Log("X");
        if (Input.GetKeyDown(KeyCode.C))
            Debug.Log("C");
    }
}
