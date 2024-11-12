using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    private bool isGrounded;
    private float horizontial;
    public float speed = 5f;
    public float velocitySpeed = 10f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector3 position;
    private Rigidbody2D rig;
    private Collider2D col;

    [SerializeField] private LayerMask jumpGround;

    private enum MovementState{ idle, run, jump, fall };
    private void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    void Update()
    {
        PlayerJump();
        PlayerMove();
        CheckAnimationState();
    }

    private void PlayerMove()
    {
        horizontial = Input.GetAxis("Horizontal");
        position = new Vector3(horizontial, 0, 0).normalized;
        transform.Translate(position * Time.deltaTime * speed);
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround())
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, velocitySpeed, 0);
        }
    }

    private void CheckAnimationState()
    {
        MovementState state;
        if (position.x > 0)
        {
            state = MovementState.run;
            spriteRenderer.flipX = false;
        }
        else if(position.x < 0)
        {
            state = MovementState.run;
            spriteRenderer.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rig.velocity.y > .1f)
        {
            state = MovementState.jump;
        }
        else if (rig.velocity.y < -.1f)
        {
            state = MovementState.fall;
        }
        animator.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            Debug.Log("ÃÅÊÇ´¥·¢Æ÷");
            collision.gameObject.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }

    private bool isGround()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }
}
