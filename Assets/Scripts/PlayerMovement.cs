using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    bool gameOver = false;
    bool stopLoseSFXRepeat = false;

    public int numCoins = 0;
    public Text gameOverText;

    public float timeLeft;
    public bool levelComplete = false;

    public ParticleSystem collectParticle;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gameOverText.text = "";
        timeLeft = 300f;
        levelComplete = false;
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

       UpdateAnimationState();

        timeLeft -= Time.deltaTime;

       if (timeLeft > 0)
       {
            timeLeft -= Time.deltaTime;
       }

       if (timeLeft <= 0 && !levelComplete)
       {
            Lose();
       }
    }

    private void UpdateAnimationState()
    {
         if (dirX > 0f)
        {
            anim.SetBool("Walking", true);
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            anim.SetBool("Walking", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void ChangeScore(int scoreAmount)
    {
        numCoins = scoreAmount;
        
        ItemCollector.instance.UpdateScore();
        if (numCoins == 5)
        {
            WinScreen();
        }
    }

    void Lose()
    {
        
    }

    void WinScreen()
    {
        gameOverText.text = "You Won! Congrats!";
        gameOver = true;
        moveSpeed = 0;
    }
}
