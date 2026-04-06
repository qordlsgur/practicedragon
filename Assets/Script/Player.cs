using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable] private struct AttackColliderOffset
    {
        public Vector2 AttackCollideroffset;
        public Vector2 NewAttackCollideroffset;
    }

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private bool MoveAnim = false;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private BoxCollider2D[] attackCollider;
    [SerializeField] private AttackColliderOffset AttackOffset;


    [SerializeField] private Vector2 MoveInput;
    private Vector2 Movedelta;

    private void Awake()
    {
        moveSpeed = 10f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackCollider = GetComponents<BoxCollider2D>();
        AttackOffset.AttackCollideroffset = attackCollider[0].offset;
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
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        MoveInput = new Vector2(x, y);
        Movedelta = MoveInput.normalized * moveSpeed * Time.deltaTime;

        if (x != 0 || y != 0)
            MoveAnim = true;
        else
            MoveAnim = false;


        if (MoveInput.x < 0)
        {
            // ¿ÞÂÊ
            spriteRenderer.flipX = true;
        }
        else if (MoveInput.x > 0)
        {
            // ¿À¸¥ÂÊ
            spriteRenderer.flipX = false;
        }

        AttackOffset.NewAttackCollideroffset.x = spriteRenderer.flipX ? -AttackOffset.AttackCollideroffset.x : AttackOffset.AttackCollideroffset.x;
        attackCollider[0].offset = AttackOffset.NewAttackCollideroffset;

        anim.SetBool("Run", MoveAnim);


    }

    private void LateUpdate()
    {
    }
}
