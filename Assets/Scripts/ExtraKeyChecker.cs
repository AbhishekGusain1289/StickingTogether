using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraKeyChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyChecker();
    }

    void KeyChecker()
    {
        if(BluePlayer.instance.hasKey)
        {
            Destroy(gameObject);
        }
    }
}
