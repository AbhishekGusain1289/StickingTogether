using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BluePlatform : MonoBehaviour
{
    Rigidbody2D BluePlatformBody;
    BoxCollider2D BluePlatformCollider;
    // Start is called before the first frame update
    void Start()
    {
        BluePlatformBody = GetComponent<Rigidbody2D>();
        BluePlatformCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(BluePlatformBody.IsTouchingLayers(LayerMask.GetMask("PinkPlayer")))
        {
            
            int level = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(LevelLoader.instance.LoadNextLevel(level));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("PinkPlayer"))
        {
            pinkPlayer.instance.Dying();
        }
    }
}
