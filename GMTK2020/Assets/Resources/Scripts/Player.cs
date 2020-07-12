using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float JUMP_FORCE;  // readonly
    [SerializeField] private float COUNTER_JUMP_FORCE;  // readonly
    private bool facingRight;

    private const float CAN_JUMP_THRESHHOLD = 0.05f;
    private const float JUMP_PRESS_BUFFER = 0.1f;
    private const float COYOTE_BUFFER = 0.1f;

    public AbilityActivation abilityActivation;
    private Ability teleport;

    private bool spacebarHeld;
    private float timeSinceGrounded;  // e.g. canJump
    private float timeSinceJumpKeyPressed; // tracks when spacebar is pressed used w/ buffering


    public Animator anim;
    [SerializeField] GameObject whiteSpace;
    [SerializeField] GameObject redSpace;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        teleport = GetComponent<Teleport>();
        anim = GetComponent<Animator>();
        facingRight = true;
        spacebarHeld = false;
        timeSinceGrounded = float.PositiveInfinity;
        timeSinceJumpKeyPressed = float.PositiveInfinity;
        rb.freezeRotation = true;

        abilityActivation = GetComponent<AbilityActivation>();
    }

    // Update is called once per frame
    void Update()
    {
        // Buffer for space presses
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeSinceJumpKeyPressed = 0f;
            // Switches from white spacebar to red spacebar
            redSpace.SetActive(true);
            whiteSpace.SetActive(false);
        }
        else
            timeSinceJumpKeyPressed += Time.deltaTime;

        // Checking if spacebar held (no buffer)
        if (Input.GetKeyDown(KeyCode.Space))
            spacebarHeld = true;
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            spacebarHeld = false;
            // Switches from red spacebar to white spacebar
            redSpace.SetActive(false);
            whiteSpace.SetActive(true);
        }

        timeSinceGrounded += Time.deltaTime; // janky :(

        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        anim.SetBool("CanJump", (timeSinceJumpKeyPressed < JUMP_PRESS_BUFFER && timeSinceGrounded < COYOTE_BUFFER));
    }

    private void FixedUpdate()
    {
        rb.velocity = Walk();
        if (timeSinceJumpKeyPressed < JUMP_PRESS_BUFFER && timeSinceGrounded < COYOTE_BUFFER)
            Jump();

        if (IsMovingUp() && !spacebarHeld) // If we're moving up and bar has been released counter force down 
            rb.AddForce(COUNTER_JUMP_FORCE * Vector2.down * rb.mass);
    }

    private Vector2 Walk()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal));
        if (horizontal != 0)
        {
            bool prev = facingRight;
            facingRight = horizontal > 0;
            int flip = prev == facingRight ? 1 : -1;
            transform.localScale = new Vector3(flip * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        return new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);
    }

    void Jump()
    {
        if (!abilityActivation.IsEnable("jump")) return;
        anim.SetTrigger("Jump");

        timeSinceGrounded = float.PositiveInfinity;  // Prevents us from double-jumping
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(JUMP_FORCE * Vector2.up * rb.mass, ForceMode2D.Impulse);
    }

    public bool CanJump()
    {
        return timeSinceJumpKeyPressed < JUMP_PRESS_BUFFER && timeSinceGrounded < COYOTE_BUFFER;
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
        // STUB
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("MagneticObject") || collision.gameObject.CompareTag("FlammableObject"))
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= CAN_JUMP_THRESHHOLD)
                {
                    timeSinceGrounded = 0f;
                    teleport.ResetAbility();
                }
            }
        }
        else if (collision.gameObject.GetComponent<Magnetic>() != null)
        {
            collision.gameObject.GetComponent<Magnetic>().Stop();
        }
    }
}
