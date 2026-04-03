using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private bool MoveAnim = false;
    
    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 MoveInput;
    private Vector2 Transform;

    private void Awake()
    {
        moveSpeed = 10f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Transform);
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        MoveInput = new Vector2 (x, y);
        Transform = MoveInput.normalized * moveSpeed * Time.deltaTime;

        if (x != 0 || y != 0)
            MoveAnim = true;
        else
            MoveAnim = false;

        anim.SetBool("Run", MoveAnim);
    }

    private void LateUpdate()
    {
    }
}
