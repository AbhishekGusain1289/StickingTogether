using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class GunScript : MonoBehaviour
{

    public static GunScript instance;


    //Muzzle Flash
    public GameObject flash;
    int FramesToFlash = 2;

    public bool isFiring=false;
    Vector2 mousePos;


    private GunControls gunController;



    public Transform shotPoint;
    [SerializeField] float BulletSpeed;
    public GameObject Bullets;

    // Start is called before the first frame update


    private void Awake()
    {
        gunController = new GunControls();

    }
    void Start()
    {
        instance = this;
        shotPoint = GetComponent<Transform>();
        flash.SetActive(false);
    }
    private void OnEnable()
    {
        gunController.Gun.Fire.performed += Shoot;
        gunController.Gun.Enable();
    }
    private void OnDisable()
    {
        gunController.Gun.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if((!DialogueManager.instance.dialoguePlaying))
        if(!UIManager.paused)
        {

        Vector2 gunPos=transform.position;
        mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction= mousePos - gunPos;
        transform.right = direction;
        }

    }

    void Shoot(InputAction.CallbackContext obj)
    {


        if(!UIManager.paused&& !DialogueManager.instance.dialoguePlaying)
        {

        isFiring = true;
        AudioManager.instance.Play("gun");
        GameObject newBullet = Instantiate(Bullets, shotPoint.position, shotPoint.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = transform.right * BulletSpeed;
        StartCoroutine(BluePlayer.instance.firing(mousePos));
        StartCoroutine(DoFlash());
        }
    }

    IEnumerator DoFlash()
    {
        flash.SetActive(true);
        var framesFlashed = 0;
        while(framesFlashed < FramesToFlash)
        {
            framesFlashed++;
            yield return null;
        }
        flash.SetActive(false);
    }
}
