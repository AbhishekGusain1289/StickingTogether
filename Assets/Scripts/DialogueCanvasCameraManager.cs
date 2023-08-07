using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueCanvasCameraManager : MonoBehaviour
{
    Canvas DialogueCanvas;
    [SerializeField] Camera BlueCam;
    [SerializeField] Camera PinkCam;
    // Start is called before the first frame update
    void Start()
    {
        DialogueCanvas=gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex==1)
        DialogueCanvas.worldCamera=BlueCam;
        else if(SceneManager.GetActiveScene().buildIndex==6)
        {
            DialogueCanvas.worldCamera = BlueCam;
        }
        else if(SceneManager.GetActiveScene().buildIndex>1)
        {
            if(CameraManger.instance.camOn==0)
            DialogueCanvas.worldCamera=BlueCam;
            else
            DialogueCanvas.worldCamera=PinkCam;
        }
    }
}
