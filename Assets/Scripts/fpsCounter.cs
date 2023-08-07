using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fpsCounter : MonoBehaviour
{
    [SerializeField] float timer, refresh, avgFramerate;
    [SerializeField] string display = "{0} fps";
    [SerializeField] Text m_text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer=timer<=0?refresh:timer-=timelapse;

        if (timer <= 0)
            avgFramerate = (int)(1f / timelapse);
        m_text.text=string.Format(display, avgFramerate.ToString());
    }
}
