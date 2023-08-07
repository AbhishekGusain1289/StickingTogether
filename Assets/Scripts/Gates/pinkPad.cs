using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinkPad : MonoBehaviour
{

    public static pinkPad instance;

    public bool PinkPressed;
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
        if (padRigidBody.IsTouchingLayers(LayerMask.GetMask("PinkPlayer")))
        {
            PadAnimator.SetBool("Pressed", true);
            PinkPressed = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((!PinkPressed) && (padRigidBody.IsTouchingLayers(LayerMask.GetMask("PinkPlayer"))))
        {
            AudioManager.instance.Play("gate");
        }
    }
}
