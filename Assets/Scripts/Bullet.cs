using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem BulletParticles;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleSystem BulletParticle = Instantiate(BulletParticles,transform.position,Quaternion.identity);
        BulletParticle.Play();
        AudioManager.instance.Play("bullet");
        Destroy(gameObject);
        cameraShake.instance.CameraShaking(3f, 0.2f);
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
