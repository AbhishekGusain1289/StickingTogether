using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy instance { get; private set; }

    public Rigidbody2D enemyBody;
    SpriteRenderer enemyRenderer;
    BoxCollider2D enemyBoxCollider;
    PolygonCollider2D enemyPolygonCollider;
    [SerializeField] float enemySpeed;


    [SerializeField] float knockbackPower;
    [SerializeField] float knockbackDuration;


    public ParticleSystem Dying;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        enemyBoxCollider = GetComponent<BoxCollider2D>();
        enemyBody = GetComponent<Rigidbody2D>();
        enemyRenderer = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        if(isFacingRight())
        {
            enemyBody.velocity = new Vector2(-enemySpeed, 0);
        }
        else
        {
            enemyBody.velocity = new Vector2(enemySpeed, 0);

        }
        Throwing();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(Mathf.Sign(enemyBody.velocity.x)*0.6f, transform.localScale.y);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    transform.localScale = new Vector2(Mathf.Sign(enemyBody.velocity.x) * 0.3f, transform.localScale.y);
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Bullet"))
        {
            Instantiate(Dying,transform.position,Quaternion.identity);
            AudioManager.instance.Play("enemyHit");
            Destroy(gameObject);
        }
        
    }

    private bool isFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    void Throwing()
    {
        if (enemyBody.IsTouchingLayers(LayerMask.GetMask("PinkPlayer")))
        {
            pinkPlayer.instance.gettingHit(transform);
        }
        else if (enemyBody.IsTouchingLayers(LayerMask.GetMask("BluePlayer")))
        {
            BluePlayer.instance.gettingHit(transform);
        }

 
    }

}
