using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Lava : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D lavaRigidBody;
    void Start()
    {
        lavaRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (lavaRigidBody.IsTouchingLayers(LayerMask.GetMask("BluePlayer")))
        {
            StartCoroutine(waiting());
            BluePlayer.instance.Dying();
        }
        if (lavaRigidBody.IsTouchingLayers(LayerMask.GetMask("PinkPlayer")))
        {
            StartCoroutine(waiting());
            pinkPlayer.instance.Dying();
        }
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(0.5f);
            int level=SceneManager.GetActiveScene().buildIndex;
            string stage="level"+level;
            StartCoroutine(AudioManager.instance.FadeOut(stage,1f));
            StartCoroutine(LevelLoader.instance.LoadNextLevel(level));
    }
}
