using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    //movement vars
    private float horizontal;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float deceleration = 30f;

    //Jump vars
    [SerializeField] private float jumpPower = 16f;
    [SerializeField] private bool onGround;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float coyoteTimeCounter;
    [SerializeField] private float jumpDelay = 0.01f;
    [SerializeField] private float jumpDelayTimer = 0;

    //sprite vars
    private bool isFacingRight = true;
    public bool isFlipped = false;

    //sound vars
    public AudioClip jumpSound;
    public float audioJStartTime;
    private AudioSource audioSource;



    public Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //constant check for horizontal input
        horizontal = Input.GetAxisRaw("Horizontal");

        //keep sprite facing correct way
        Flip();

        //slight delay after jump before can jump again (so that coyotetime doesn't allow double jump)
        if (jumpDelayTimer > 0)
        {
            jumpDelayTimer -= Time.deltaTime;
        }


        //coyote time implementation
        if (onGround)
        {
            //coyote timer only resets when the jump delay timer is done (aka cant jump again until delay)
            if (jumpDelayTimer <= 0)
            {
                coyoteTimeCounter = coyoteTime;
            }
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (!isFlipped)
        {
            if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f)
            {
                audioSource.PlayOneShot(jumpSound);
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                coyoteTimeCounter = 0;
                jumpDelayTimer = jumpDelay;
            }
            if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f)
            {
                audioSource.PlayOneShot(jumpSound);
                rb.velocity = new Vector2(rb.velocity.x, -jumpPower);
                coyoteTimeCounter = 0f;
                jumpDelayTimer = jumpDelay;
            }
            if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y < 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }

        
    }

    private void FixedUpdate()
    {
        CheckGrounded();

        float targetSpeed = horizontal * maxSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;

        // Choose accel/decel depending on input
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;

        // Calculate the amount to change velocity this frame
        float movement = Mathf.Clamp(speedDiff * accelRate * Time.fixedDeltaTime, -Mathf.Abs(speedDiff), Mathf.Abs(speedDiff));

        rb.velocity = new Vector2(rb.velocity.x + movement, rb.velocity.y);

    }

    private void CheckGrounded()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void FlipGravity()
    {
        rb.gravityScale *= -1;

        // Flip sprite vertically
        Vector3 scale = this.transform.localScale;
        scale.y *= -1;
        this.transform.localScale = scale;
        isFlipped = !isFlipped;
    }
}
