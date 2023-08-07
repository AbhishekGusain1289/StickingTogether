using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalPad : MonoBehaviour
{
    public static normalPad instance;
    public bool pressed;
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
        if(padRigidBody.IsTouchingLayers(LayerMask.GetMask("PinkPlayer")))
        {
            PadAnimator.SetBool("Pressed", true);
            pressed = true;
        }
        else if(padRigidBody.IsTouchingLayers(LayerMask.GetMask("BluePlayer")))
        {
            PadAnimator.SetBool("Pressed", true);
            pressed = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((!pressed) & ((padRigidBody.IsTouchingLayers(LayerMask.GetMask("BluePlayer"))|| (padRigidBody.IsTouchingLayers(LayerMask.GetMask("PinkPlayer"))))))
        {
            AudioManager.instance.Play("gate");
        }
    }
}
