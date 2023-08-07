using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinkPlatform : MonoBehaviour
{
    Rigidbody2D PinkPlatformBody;
    BoxCollider2D PinkPlatformCollider;
    // Start is called before the first frame update
    void Start()
    {
        PinkPlatformBody = GetComponent<Rigidbody2D>();
        PinkPlatformCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PinkPlatformBody.IsTouchingLayers(LayerMask.GetMask("BluePlayer")))
        {
            int level = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(LevelLoader.instance.LoadNextLevel(level));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("BluePlayer"))
        {
            BluePlayer.instance.Dying();
        }
    }
}
