using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailKey : MonoBehaviour
{
    Animator keyAnimator;
    BoxCollider2D keyCollider;
    // Start is called before the first frame update
    void Start()
    {
        keyAnimator = GetComponent<Animator>();
        keyCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BluePlayer"))
        {
            keyAnimator.SetTrigger("Destroy");
            StartCoroutine(DestroyingKey());
            AudioManager.instance.Play("key");
        }
        IEnumerator DestroyingKey()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}
