using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class pinkPlayer : MonoBehaviour
{

    public static pinkPlayer instance;

    public GameObject BluePlayerBody;

    public static bool inJail = true;
    public bool hasKey=false;

    Rigidbody2D pinkPlayerBody;
    BoxCollider2D pinkPlayerCollider;
    [SerializeField] float Speed;
    [SerializeField] float JumpForce;
    Animator pinkPlayerAnimator;
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject GroundCheck;
    bool facingRight = true;

    public bool HitPinkPlayer;

    public InputAction movement;
    private PlayerControls Controller;
    float flip;


    //Death Particles
    [SerializeField] ParticleSystem DeathParticle;


    [SerializeField] Vector2 knockbackPower;
    public bool beingHit=false;


    int canJump;
    // Start is called before the first frame update


    private void Awake()
    {
        Controller=new PlayerControls();
        HitPinkPlayer=false;

        
    }



    void Start()
    {
        instance = this;
        pinkPlayerBody = GetComponent<Rigidbody2D>();
        pinkPlayerCollider = GetComponent<BoxCollider2D>();
        pinkPlayerAnimator = GetComponent<Animator>();
        if(inJail)
        {
            pinkPlayerBody.isKinematic = true;
            pinkPlayerCollider.enabled = true;
            Arrow.SetActive(false);
        }
    }


    private void OnEnable()
    {

        movement = Controller.Player.Movement;
        movement.Enable();

        Controller.Player.Jump.performed += pinkPlayerMovement;
        Controller.Player.Enable();

    }

    private void OnDisable()
    {
        movement.Disable();
        Controller.Player.Jump.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        
        flip = movement.ReadValue<Vector2>().x;

        if(!inJail)
        {
            pinkPlayerBody.isKinematic = false;
            pinkPlayerCollider.enabled = true;
            Arrow.SetActive(true);

        }
        changeAnimation();
        KeyChecker();
        GroundChecker();
        enemyChecker();
        if (pinkPlayerBody.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            beingHit = true;
        }
        DialogueChecker();

    }

    private void DialogueChecker()
    {
        if (DialogueManager.instance.dialoguePlaying)
        {
            pinkPlayerAnimator.SetBool("Walk", false);
            pinkPlayerBody.velocity = new Vector2(0f, pinkPlayerBody.velocity.y);
        }
        else
        {
            pinkPlayerBody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void FixedUpdate()
    {
        if(!inJail)
        {
            pinkPlayerMovements();

            if (facingRight == false && flip > 0)
            {
                flipPlayer();
            }
            else if (facingRight == true && flip < 0)
            {
                flipPlayer();
            }

        }
    }


    private void pinkPlayerMovements()
    {
        if(!DialogueManager.instance.dialoguePlaying)
        if(!beingHit)
        {
            Vector2 velocity = movement.ReadValue<Vector2>() * Speed;
            pinkPlayerBody.velocity = new Vector2(velocity.x,pinkPlayerBody.velocity.y);
        }
    }
    void changeAnimation()
    {
        if(!DialogueManager.instance.dialoguePlaying)
        {

        if (flip > 0 || flip < 0)
        {
            pinkPlayerAnimator.SetBool("Walk", true);
        }
        else
            pinkPlayerAnimator.SetBool("Walk", false);
        }
    }
    void enemyChecker()
    {
        if (pinkPlayerBody.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            cameraShake.instance.CameraShaking(5f, 0.2f);
            HitPinkPlayer = true;
        }
        else
        {
            HitPinkPlayer = false;
        }
    }
    void flipPlayer()
    {
        if (!DialogueManager.instance.dialoguePlaying)
        {

            facingRight = !facingRight;
        Vector3 scalar = transform.localScale;
        scalar.x *= -1;
        transform.localScale = scalar;
        }
    }

    private void pinkPlayerMovement(InputAction.CallbackContext obj)
    {
        if (obj.performed && canJump == 1 && (!DialogueManager.instance.dialoguePlaying))
        {
            pinkPlayerBody.AddForce(Vector2.up*JumpForce, ForceMode2D.Impulse);
            canJump -= 1;
            pinkPlayerAnimator.SetTrigger("Jump");

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Gate") && hasKey == true)
        {
            ExitGate.instance.ColorChanger();
            ExitGate.instance.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(LevelLoader.instance.LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
            AudioManager.instance.Play("gate");
        }
        if (collision.collider.CompareTag("Lava"))
        {
            pinkPlayerAnimator.enabled = false;
            pinkPlayerBody.bodyType = RigidbodyType2D.Static;
            this.enabled = false;
            AudioManager.instance.Play("hurt");
        }
    }
    private void GroundChecker()
    {
        if (GroundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")) || GroundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("BluePlayer"))||GroundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            canJump = 1;
            StartCoroutine(waiting());
            beingHit = false;
        }
        else
            canJump = 0;
    }


    void KeyChecker()
    {
        if (pinkPlayerBody.IsTouchingLayers(LayerMask.GetMask("Key")))
        {
            hasKey = true;
        }
    }

    public void gettingHit(Transform enemy)
    {
        Vector2 dir=(this.transform.position-enemy.position).normalized;
        pinkPlayerBody.velocity =knockbackPower*dir;
    }


    IEnumerator waiting()
    {
        yield return new WaitForSeconds(1f);

    }

    public void Dying()
    {

        ParticleSystem deathparticles = Instantiate(DeathParticle, transform.position, Quaternion.identity);
        deathparticles.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            LivesManager.instance.HitChecker();
            AudioManager.instance.Play("hurt");

        }
    }
    public void DyingFinally()
    {
        pinkPlayerAnimator.enabled = false;
        pinkPlayerBody.bodyType = RigidbodyType2D.Static;
        this.enabled = false;
    }
}
