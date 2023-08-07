using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System;

public class BluePlayer : MonoBehaviour
{
    
    public static BluePlayer instance;

    bool hasJailKey = false;
    [SerializeField] float gunKnockback;


    public Rigidbody2D bluePlayerBody;
    BoxCollider2D bluePlayerCollider;
    [SerializeField] float Speed;
    [SerializeField] float JumpForce;
    Animator bluePlayerAnimator;
    float flip;
    int canJump = 1;
    bool facingRight = true;
    [SerializeField] GameObject GroundCheck;
    public ParticleSystem Dust;


    public bool HitPlayer=false;

    //Knockback
    [SerializeField] Vector2 knockbackPower;
    public bool beingHit = false;


    public InputAction movement;
    private PlayerControls Controller;
    Vector2 hor;

    //Death Particles
    [SerializeField] ParticleSystem DeathParticle;


    //Shooting Bullets
    public GameObject Bullet;
    public Transform Gun;


    //Opening gate

    public Light Gatelight;
    public Rigidbody2D GateRigidBody;

    public bool hasKey = false;
    // Start is called before the first frame update
    void Start()
    {
        instance= this;
        bluePlayerBody = GetComponent<Rigidbody2D>();
        Gatelight = GetComponent<Light>();
        bluePlayerCollider = GetComponent<BoxCollider2D>();
        bluePlayerAnimator=GetComponent<Animator>();
        bluePlayerBody.inertia = 0.166667f;
        
    }
    private void Awake()
    {
        Controller = new PlayerControls();

    }

    // Update is called once per frame
    private void OnEnable()
    {
        movement = Controller.Player.Movement;
        movement.Enable();

        Controller.Player.Jump.performed += BluePlayerMovement;
        Controller.Player.Enable();

    }
    private void OnDisable()
    {
        movement.Disable();
        Controller.Player.Jump.Disable();
    }
    void Update()
    {
        KeyChecker();
        if((!DialogueManager.instance.dialoguePlaying))
        changeAnimation();
        GroundChecker();
        flip = movement.ReadValue<Vector2>().x;
        if(bluePlayerBody.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            beingHit = true;
        }
        enemyChecker();
        DialogueChecker();
    }

    private void DialogueChecker()
    {
        if(DialogueManager.instance.dialoguePlaying)
        {
            bluePlayerAnimator.SetBool("Walk", false);
            bluePlayerBody.velocity = new Vector2(0f, bluePlayerBody.velocity.y);
        }
        else
        {
            bluePlayerBody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void FixedUpdate()
    {
        bluePlayerMovements();

        if (facingRight == false && flip > 0)
        {
            flipPlayer();
        }
        else if (facingRight == true && flip < 0)
        {
            flipPlayer();
        }

    }

    private void BluePlayerMovement(InputAction.CallbackContext obj)
    {
        if (obj.performed && canJump == 1&&(!DialogueManager.instance.dialoguePlaying))
        {
            bluePlayerBody.AddForce(Vector3.up*JumpForce, ForceMode2D.Impulse);
            canJump -= 1;
            bluePlayerAnimator.SetTrigger("Jump");
        }
    }
    void flipPlayer()
    {
        if((!DialogueManager.instance.dialoguePlaying))
        { 
            facingRight = !facingRight;
            Vector3 scalar = transform.localScale;
            scalar.x *= -1;
            transform.localScale = scalar;
            GunScript.instance.transform.localScale = scalar * 0.15f;
        }
    }
    public void bluePlayerMovements()
    {
        if((!beingHit)&&(!GunScript.instance.isFiring)&&(!DialogueManager.instance.dialoguePlaying))
        {
            Vector2 velocity= movement.ReadValue<Vector2>() * Speed;
            bluePlayerBody.velocity=new Vector2(velocity.x,bluePlayerBody.velocity.y);
        }
        
    }

    void enemyChecker()
    {
        if(bluePlayerBody.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            HitPlayer = true;
            cameraShake.instance.CameraShaking(3f, 0.2f);
            

        }
        else
        {
            HitPlayer = false;
        }
    }

    void KeyChecker()
    {
        if (bluePlayerBody.IsTouchingLayers(LayerMask.GetMask("Key")))
        {
            hasKey = true;
        }
        if(bluePlayerBody.IsTouchingLayers(LayerMask.GetMask("JailKey")))
        {
            hasJailKey = true;
        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Gate")&&hasKey==true)
        {
            ExitGate.instance.ColorChanger();
            ExitGate.instance.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(LevelLoader.instance.LoadNextLevel(SceneManager.GetActiveScene().buildIndex+1));
            AudioManager.instance.Play("gate");


        }
        if(collision.collider.CompareTag("Jail")&& hasJailKey==true)
        {
            Jail.instance.DestroyingJail();
            AudioManager.instance.freeMusic();
        }
        if(collision.collider.CompareTag("Lava"))
        {
            bluePlayerAnimator.enabled = false;
            bluePlayerBody.bodyType = RigidbodyType2D.Static;
            this.enabled = false;
            AudioManager.instance.Play("hurt");
        }
        

        

    }


    private void GroundChecker()
    {
        if (GroundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))|| GroundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("PinkPlayer"))|| GroundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            canJump = 1;
            StartCoroutine(waiting());
            beingHit = false;
            
        }
        else
            canJump = 0;
    }
    


    void changeAnimation()
    {
        if (flip > 0 || flip < 0)
        {
            bluePlayerAnimator.SetBool("Walk", true);
        }
        else
            bluePlayerAnimator.SetBool("Walk", false);
    }

    public void gettingHit(Transform enemy)
    {
        Vector2 dir=(this.transform.position-enemy.position).normalized;
        bluePlayerBody.velocity = new Vector2(bluePlayerBody.velocity.x, bluePlayerBody.velocity.y);

        bluePlayerBody.velocity = knockbackPower *(dir);
         
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(1f);

    }

    public IEnumerator firing(Vector2 mousepos)
    {
        Vector2 bluepos = transform.position;
        Vector2 pos=(bluepos-mousepos).normalized;
        if(canJump==1)
        {
            bluePlayerBody.velocity = new Vector2(2f * pos.x, bluePlayerBody.velocity.y);
            yield return new WaitForSeconds(0.2f);
        }
        else
        {
            bluePlayerBody.velocity = new Vector2(gunKnockback*pos.x, bluePlayerBody.velocity.y);
            yield return new WaitForSeconds(0.7f);

        }
        GunScript.instance.isFiring=false;
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
        bluePlayerAnimator.enabled = false;
        bluePlayerBody.bodyType = RigidbodyType2D.Static;
        this.enabled = false;
    }
}
