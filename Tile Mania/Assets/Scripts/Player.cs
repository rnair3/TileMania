using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(250f, 250f);

    //state
    bool isAlive = true;
    bool blinking = false;
    float totalBlinkTime = 5f;
    float changeTime = 1f;


    //Cache
    Rigidbody2D rigidbody;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();

        gravityScale = rigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        Jump();
        FlipSide();
        ClimbLadder();
        Death();

        //if (blinking)
        //{
        //    SpriteBlinkEffect();
        //}
    }

    public void Run()
    {
        float controlFlow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(controlFlow*speed, rigidbody.velocity.y);
        rigidbody.velocity = velocity;

        bool playerHasHorizontalVelocity = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isWalking", playerHasHorizontalVelocity);
    }

    private void Jump()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            rigidbody.velocity += jumpVelocity;
        }
    }

    private void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            animator.SetBool("willClimb", false);
            rigidbody.gravityScale = gravityScale;
            return;
        }

        float controlFlow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 velocity = new Vector2(rigidbody.velocity.x, controlFlow * climbSpeed);
        rigidbody.velocity = velocity;
        rigidbody.gravityScale = 0f;

        bool playerHasVerticalVelocity = Mathf.Abs(rigidbody.velocity.y) > Mathf.Epsilon;
        animator.SetBool("willClimb", playerHasVerticalVelocity);

    }

    private void FlipSide()
    {
        bool playerHasHorizontalVelocity = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalVelocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody.velocity.x), 1f);
        }
    }


    void Death()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            animator.SetTrigger("Death");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            isAlive = false;
            FindObjectOfType<GameSession>().ProcessDeath();
           // Destroy(gameObject);
        }
    }

    private void SpriteBlinkEffect()
    {
        float blinkTime = Time.deltaTime;
        if(blinkTime > totalBlinkTime)
        {
            blinking = false;
        }
        else if(blinkTime - Time.deltaTime >= changeTime)
        {
            blinkTime = Time.deltaTime;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        
    }
}
