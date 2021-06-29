using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce;
    public float secondJumpForce;
    public float gravityModifier;
    public bool gameOver = false;
    public ParticleSystem collisionFX;
    public ParticleSystem runningFX;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    private bool allowDoubleJump;
    private bool isOnGround;
    private float normalSpeed;
    private Rigidbody playerRB;
    private Animator anim;
    private AudioSource playerAudio;
    private GameManager gameManager;

    private void Awake()
    {
        Physics.gravity = new Vector3(0, -9.8F, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Physics.gravity *= gravityModifier;
        normalSpeed = speed;

        runningFX.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            ClickSpaceToJump();
            ClickAToBoost();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            allowDoubleJump = true;
            StartRunning();
        }
        else if (collision.gameObject.CompareTag("Obstacles") && !gameOver)
        {
            gameOver = true;
            allowDoubleJump = false;
            print("Game over");
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
            runningFX.Stop();
            collisionFX.Play();
            playerAudio.PlayOneShot(crashSound);
        }
    }

    public void StartRunning()
    {
        anim.SetFloat("Speed_f", speed);
        anim.SetBool("Static_b", true);
        runningFX.Play();
        isOnGround = true;
        allowDoubleJump = true;
    }

    void ClickSpaceToJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            allowDoubleJump = true;
            anim.SetTrigger("Jump_trig");
            runningFX.Stop();
            playerAudio.PlayOneShot(jumpSound);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && allowDoubleJump)
        {
            playerRB.velocity = Vector3.zero;
            playerRB.AddForce(Vector3.up * secondJumpForce, ForceMode.Impulse);
            allowDoubleJump = false;
            anim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound);
        }
    }
    void ClickAToBoost() {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.speed *= 3f;
            speed = normalSpeed * 3;
            gameManager.scoreBoostRate = 2;
            gameManager.score_text.fontSize = 35f;
            gameManager.score_text.color = Color.green;
        }
        
        if(Input.GetKeyUp(KeyCode.A))
        {
            anim.speed = 1;
            speed = normalSpeed;
            gameManager.score_text.fontSize = 25f;
            gameManager.score_text.color = Color.white;
        }
    }
}
