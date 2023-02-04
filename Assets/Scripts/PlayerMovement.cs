using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jump;
    public float dash;
    public float dashDuration;
    public float dashRechargeDuration;

    private Rigidbody2D rb;
    private bool isOnGround;
    private bool isJumping;
    private float jumpStart = 0f;
    private bool hasUsedDashDuringJump = false;
    private bool isDashing;
    private float dashStart = 0f;
    private bool isMovingRight = true;

    private AnimationCurve jumpVelocityAnimation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        jumpVelocityAnimation = new  AnimationCurve(
            new Keyframe(0, 1),
            new Keyframe(0.35f, 1),
            new Keyframe(0.6f, 0),
            new Keyframe(1.2f, -1.5f),
            new Keyframe(1.8f, -2f)); // "Smooth" jump. haha
        jumpVelocityAnimation.SmoothTangents(0, 0);
        jumpVelocityAnimation.SmoothTangents(2, 0);
        jumpVelocityAnimation.SmoothTangents(4, 0);
        jumpVelocityAnimation.postWrapMode  = WrapMode.ClampForever;
    }

    // Update is called once per frame
    void Update()
    {
        // ***** Movement. *****

        if (!this.isDashing) {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        }

        // ***** Jump. *****

        if (Input.GetButtonDown("Jump") && isOnGround) {
            this.isJumping = true;
            this.isDashing = false;
            this.jumpStart = Time.time;
        }

        if (Input.GetButtonUp("Jump") && this.isJumping && Time.time < this.jumpStart + 0.75f) {
            this.jumpStart = Time.time - 0.65f; // jump to the middle of the jump animation
        }

        if (this.isJumping) {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocityAnimation.Evaluate(Time.time - this.jumpStart) * jump);
        }

        // ***** Dash. *****

        if (Input.GetButtonDown("Dash")
            && Time.time > this.dashStart + this.dashRechargeDuration // wait for recharge between dashes
            && !this.hasUsedDashDuringJump) { // only one dash per jump
            this.isDashing = true;
            this.dashStart = Time.time;
            this.isJumping = false;
            if (!this.isOnGround) {
                this.hasUsedDashDuringJump = true;
            }
        }

        if (this.isDashing) {
            rb.velocity = new Vector2((this.isMovingRight ? 1 : -1) * this.dash, 0);

            if (Time.time > this.dashStart + this.dashDuration) {
                this.isDashing = false;
            }
        }

        if (rb.velocity.x != 0) {
            this.isMovingRight = rb.velocity.x > 0;
        }

        // ***** Other. *****

        if (rb.velocity.y < -2f * jump) { // terminal velocity.
            rb.velocity = new Vector2(rb.velocity.x, -2f * jump);
        }
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            this.isOnGround = true;
            this.isJumping = false;
            this.hasUsedDashDuringJump = false;
        }
    }

    public void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            this.isOnGround = false;
        }
    }

    public void Reset() {
        rb.velocity = new Vector2();
        this.isJumping = false;
        this.isDashing = false;
        this.dashStart = Time.time;
        this.hasUsedDashDuringJump = false;
    }
}
