using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    private enum playerState
    {
        alive,
        die
    }
    private bool isGrounded;
    private float horizontial;
    public float speed = 5f;
    public float velocitySpeed = 10f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector3 position;
    private Rigidbody2D rig;
    private Collider2D col;
    private playerState state;
    private Scene nowScenen;

    [SerializeField] private LayerMask jumpGround;

    private enum MovementState{ idle, run, jump, fall };
    private void Start()
    {
        nowScenen = SceneManager.GetActiveScene();
        state = playerState.alive;
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
        if (transform.position.y < -12f)
        {
            state = playerState.die;
        }
        if (state == playerState.die)
        {
            SceneManager.LoadScene(nowScenen.name, LoadSceneMode.Single);
        }
    }

    private void PlayerMove()
    {
        horizontial = Input.GetAxis("Horizontal");
        position = new Vector3(horizontial, 0, 0).normalized;
        rig.velocity = new Vector2 (position.x * velocitySpeed, rig.velocity.y);
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround())
        {
            rig.velocity = new Vector3(0, velocitySpeed, 0);
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
            SceneManager.LoadScene(nowScenen.buildIndex + 1, LoadSceneMode.Single);
        }
    }

    private bool isGround()
    {
        
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }
}
