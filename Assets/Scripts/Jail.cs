using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jail : MonoBehaviour
{

    public static Jail instance;
    public GameObject pinkPlayerBody;

    public ParticleSystem Party1;
    public ParticleSystem Party2;

    Animator jailAnimator;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        jailAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyingJail()
    {
        pinkPlayer.inJail = false;
        jailAnimator.SetBool("Destroy",true);
        StartCoroutine(waiting());
        Party1.Play();
        Party2.Play();
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
