using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BluePadGate : MonoBehaviour
{
    Light2D GateLight;
    // Start is called before the first frame update
    void Start()
    {
        GateLight = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bluePad.instance.BluePressed == true)
        {
            GateLight.color = Color.white;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
