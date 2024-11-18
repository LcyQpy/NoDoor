using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private enum playerState
    {
        alive,
        die
    }
    private bool isGrounded;
    private float horizontial;
    public float velocitySpeedX = 10f;
    public float velocitySpeedY = 10f;
    private bool hasKey = false;
    private Animator animator;
    private float dirX;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Vector2 te;
    private Rigidbody2D rig;
    private Collider2D col;
    private playerState state;
    private Scene nowScenen;
    public AudioSource audioSource;
    public AudioClip running;


    [SerializeField] private LayerMask jumpGround;

    private enum MovementState{ idle, run, jump, fall };
    private void Start()
    {
        hasKey = false;
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
        dirX = horizontial;
        rig.velocity = new Vector2 (horizontial * velocitySpeedX, rig.velocity.y);
        te = rig.velocity;
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround())
        {
            rig.velocity = new Vector2(0, velocitySpeedY);
        }
    }

    private void CheckAnimationState()
    {
        MovementState state;
        if (dirX > 0)
        {
            state = MovementState.run;
            spriteRenderer.flipX = false;
        }
        else if(dirX < 0)
        {
            state = MovementState.run;
            audioSource.clip = running;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
            spriteRenderer.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rig.velocity.y > .1f && !isGround())
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
        Debug.Log(collision.name);
        if (collision.gameObject.tag == "Door" && hasKey)
        {
            GameManager.Instance.GameOver();
        }
        else if(collision.gameObject.tag == "Key")
        {
            hasKey = true;
            collision.gameObject.transform.localScale = Vector2.MoveTowards(collision.gameObject.transform.localScale, Vector2.zero, 10f);
            Destroy(collision.gameObject);
        }else if(collision.gameObject.tag == "Trap")
        {
            GameManager.Instance.ReloadLevel();
        }
    }

    private bool isGround()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }
}
