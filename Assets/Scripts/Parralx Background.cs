using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralxBackground : MonoBehaviour
{
    [SerializeField] private float ParallaxEffectMultiplier;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition=cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement=cameraTransform.position-lastCameraPosition;
        ParallaxEffectMultiplier = 0.5f;
        transform.position+=new Vector3( deltaMovement.x*ParallaxEffectMultiplier, deltaMovement.y * ParallaxEffectMultiplier);
        lastCameraPosition=cameraTransform.position;
    }
}
