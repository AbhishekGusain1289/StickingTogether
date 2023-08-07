using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluePad : MonoBehaviour
{
    public static bluePad instance;


    public bool BluePressed = false;
    Animator PadAnimator;
    Rigidbody2D padRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        padRigidBody = GetComponent<Rigidbody2D>();
        PadAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (padRigidBody.IsTouchingLayers(LayerMask.GetMask("BluePlayer")))
        {
            PadAnimator.SetBool("Pressed", true);
            BluePressed = true;
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((!BluePressed)&&(padRigidBody.IsTouchingLayers(LayerMask.GetMask("BluePlayer"))))
        {
            AudioManager.instance.Play("gate");
        }
    }
}
