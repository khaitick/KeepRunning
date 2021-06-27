using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public bool gameOver = false;
    public ParticleSystem collisionFX;
    public ParticleSystem runningFX;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    private float speed;
    private bool isOnGround;
    private Rigidbody playerRB;
    private Animator anim;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        speed = FindObjectOfType<MoveLeft>().speed;
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            anim.SetTrigger("Jump_trig");
            runningFX.Stop();
            playerAudio.PlayOneShot(jumpSound);
        }
        //transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            anim.SetFloat("Speed_f", speed);
            anim.SetBool("Static_b", true);
            runningFX.Play();
            print("Ground");
        }
        else if (collision.gameObject.CompareTag("Obstacles"))
        {
            gameOver = true;
            print("Game over");
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
            runningFX.Stop();
            collisionFX.Play();
            playerAudio.PlayOneShot(crashSound);
        }
    }
}
