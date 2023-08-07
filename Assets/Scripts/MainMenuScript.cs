using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        StartCoroutine(LevelLoader.instance.LoadNextLevel(1));
        StartCoroutine(AudioManager.instance.FadeOut("menu",1f));
    }

    public void Quit()
    {
        Application.Quit();
    }


}
