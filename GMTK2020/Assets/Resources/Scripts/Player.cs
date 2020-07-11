using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float JUMP_FORCE;  // readonly
    [SerializeField] private float COUNTER_JUMP_FORCE;  // readonly

    [SerializeField] private float JUMP_TIMER = 0.1f;
    private float jumpTimerLeft;
    private bool facingRight;
    private float isGrounded;  // i.e. canJump
    private bool isJumping;
    private bool jumpKeyHeld;

    private const float CAN_JUMP_THRESHHOLD = 0.05f;
    private const float JUMP_BUFFER = 0.1f;
    private const float COYOTE_BUFFER = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        facingRight = true;
        jumpTimerLeft = 0f;
        isGrounded = 0f;
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimers();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpKeyHeld = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyHeld = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) || jumpTimerLeft > 0)
        {
            if (jumpTimerLeft <= 0)
            {
                jumpTimerLeft = JUMP_TIMER;
            }

            if (!isJumping && isGrounded < COYOTE_BUFFER)
            {
                Jump();
                jumpTimerLeft = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Walk();
        if (isJumping && IsMovingUp() && !jumpKeyHeld)
        {
            rb.AddForce(COUNTER_JUMP_FORCE * Vector2.down * rb.mass);
        }
    }

    private void UpdateTimers()
    {
        if (jumpTimerLeft > 0) jumpTimerLeft -= Time.deltaTime;
        isGrounded += Time.deltaTime;
    }

    Vector2 Walk()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        return new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);
    }

    void Jump()
    {
        isJumping = true;
        isGrounded = COYOTE_BUFFER;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(JUMP_FORCE * Vector2.up * rb.mass, ForceMode2D.Impulse);
    }

    private bool IsMovingUp()
    {
        return Vector2.Dot(rb.velocity, Vector2.up) > 0;
    }

    public bool IsRight()
    {
        return facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= CAN_JUMP_THRESHHOLD)
                {
                    isGrounded = 0;  // true
                }
            }
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Floor"))
    //    {
    //        isGrounded = false;
    //    }
    //}
}
