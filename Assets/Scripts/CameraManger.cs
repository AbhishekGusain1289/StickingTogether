using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraManger : MonoBehaviour
{
    public static CameraManger instance;

    public GameObject BluePlayerCam;
    public GameObject PinkPlayerCam;

    public GameObject Gun;
    public float camOn = 0;

    public GameObject pinkPlayerBody;
    public GameObject bluePlayerBody;
    [SerializeField] GameObject BlueArrow;
    [SerializeField] GameObject PinkArrow;


    private PlayerControls Controller;
    public Image SwitchKey;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        Controller =new PlayerControls();
        pinkPlayerBody.GetComponent<pinkPlayer>().enabled=false;
        PinkPlayerCam.SetActive(false);
        if(!pinkPlayer.inJail)
        {
            BlueArrow.SetActive(true);
        }


    }
    private void OnEnable()
    {
        Controller.Switch.Change.performed += Switching;
        Controller.Switch.Enable();
    }
    private void OnDisable()
    {
        Controller.Switch.Disable();
    }

    private void Switching(InputAction.CallbackContext obj)
    {
        if(!DialogueManager.instance.dialoguePlaying)
        if (!pinkPlayer.inJail)
        {
            if (obj.performed)
            {

                if (camOn == 0)
                {
                    Gun.GetComponent<GunScript>().enabled = false;
                    bluePlayerBody.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bluePlayerBody.GetComponent<Rigidbody2D>().velocity.y);
                    bluePlayerBody.GetComponent<Animator>().SetBool("Walk",false);
                    bluePlayerBody.GetComponent<BluePlayer>().enabled = false;
                    pinkPlayerBody.GetComponent<pinkPlayer>().enabled = true;
                    BluePlayerCam.SetActive(false);
                    PinkPlayerCam.SetActive(true);
                    PinkArrow.SetActive(true);
                    BlueArrow.SetActive(false);

                    camOn = 1;
                }
                else if (camOn == 1)
                {
                    Gun.GetComponent<GunScript>().enabled = true;
                    pinkPlayerBody.GetComponent<Rigidbody2D>().velocity = new Vector2(0, pinkPlayerBody.GetComponent<Rigidbody2D>().velocity.y);
                    pinkPlayerBody.GetComponent<Animator>().SetBool("Walk", false);
                    bluePlayerBody.GetComponent<Rigidbody2D>().drag = 0f;
                    bluePlayerBody.GetComponent<Rigidbody2D>().angularDrag = 0f;
                    BluePlayerCam.SetActive(true);
                    PinkPlayerCam.SetActive(false);
                    bluePlayerBody.GetComponent<BluePlayer>().enabled = true;
                    pinkPlayerBody.GetComponent<pinkPlayer>().enabled = false;
                    camOn = 0;
                    PinkArrow.SetActive(false);
                    BlueArrow.SetActive(true);
                }

            }
            

        }
    }

    void Start()
    {
        instance = this;
        PinkArrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(pinkPlayer.inJail)
            SwitchKey.gameObject.SetActive(false);
        else
        {

            SwitchKey.gameObject.SetActive(true);
            BluesArrow();
        }
        if ((bluePlayerBody.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("PinkPlayer"))|| bluePlayerBody.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Box"))) && camOn==1)
        {
            bluePlayerBody.GetComponent<Rigidbody2D>().angularDrag = 1000f;
            bluePlayerBody.GetComponent<Rigidbody2D>().drag = 1000f;

        }
        else
        {
            bluePlayerBody.GetComponent<Rigidbody2D>().angularDrag = 0f;
            bluePlayerBody.GetComponent<Rigidbody2D>().drag = 0f;
        }

        if((pinkPlayerBody.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("BluePlayer")) || pinkPlayerBody.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Box"))||pinkPlayerBody.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Bullet"))) && camOn == 0)
        {
            pinkPlayerBody.GetComponent<Rigidbody2D>().angularDrag = 1000f;
            pinkPlayerBody.GetComponent<Rigidbody2D>().drag = 1000f;
        }
        else
        {
            pinkPlayerBody.GetComponent<Rigidbody2D>().angularDrag = 0f;
            pinkPlayerBody.GetComponent<Rigidbody2D>().drag = 0f;
        }

    }
    private void BluesArrow()
    {
        if(pinkPlayer.inJail)
        {
            BlueArrow.SetActive(false);
        }
    }
}
