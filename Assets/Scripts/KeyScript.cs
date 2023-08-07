using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    BoxCollider2D keyCollider;
    Animator keyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        keyCollider = GetComponent<BoxCollider2D>();
        keyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BluePlayer"))
        {
            keyAnimator.SetTrigger("Destroy");
            AudioManager.instance.Play("key");
            StartCoroutine(DestroyingKey());
        }
        if (collision.CompareTag("PinkPlayer"))
        {
            keyAnimator.SetTrigger("Destroy");
            AudioManager.instance.Play("key");
            StartCoroutine(DestroyingKey());
        }
    }
    IEnumerator DestroyingKey()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
