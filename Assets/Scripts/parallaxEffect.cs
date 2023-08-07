using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxEffect : MonoBehaviour
{
    public Camera BluePlayerCam;
    public Camera PinkPlayerCam;
    // Start is called before the first frame update
    [SerializeField] private float ParallaxEffectMultiplier;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        //cameraTransform = mainCam.transform;
        //lastCameraPosition = mainCam.transform.position;
    }

    // Update is called once per frame

    private void Update()
    {
        CamerChecker();
    }

    private void CamerChecker()
    {
        if(BluePlayerCam.isActiveAndEnabled)
        {
            mainCam=BluePlayerCam;
        }
        else
        {
            mainCam = PinkPlayerCam;
        }
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = mainCam.transform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * ParallaxEffectMultiplier, deltaMovement.y * ParallaxEffectMultiplier);
        lastCameraPosition = mainCam.transform.position;
    }
}
