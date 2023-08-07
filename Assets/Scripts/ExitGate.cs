using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class ExitGate : MonoBehaviour
{
    public static ExitGate instance;
    Light2D GateLight;
    Rigidbody2D gateRigidBody;

    public Canvas NextLevelLoading;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        GateLight = GetComponent<Light2D>();
    }

    // Update is called once per frame

    public void ColorChanger()
    {
        
        GateLight.color = Color.white;
    }
}
