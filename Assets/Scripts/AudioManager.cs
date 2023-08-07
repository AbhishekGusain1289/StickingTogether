using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager:MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] Sounds;
    // Start is called before the first frame update
    void Awake()
    {
        instance=this;
        foreach (Sound s in Sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip=s.clip;
            s.source.volume=s.volume;
            s.source.pitch=s.pitch;
            s.source.loop=s.loop;
        }
    }
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex==0)
        {
            Play("menu");
        }

        else if(SceneManager.GetActiveScene().buildIndex==1)
        {
            Play("level1");

        }
        else if(SceneManager.GetActiveScene().buildIndex==2)
        {
            Play("level2");
        }
        else if(SceneManager.GetActiveScene().buildIndex==3)
        {
            Play("level3");
        }
        else if(SceneManager.GetActiveScene().buildIndex==4)
        {
            Play("level4");
        }
        else if(SceneManager.GetActiveScene().buildIndex==5)
        {
            Play("level5");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            Play("level6");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string name)
    {
        Sound s=Array.Find(Sounds, s => s.name==name);
        s.source.Play();
    }

    public IEnumerator FadeOut(string name,float duration)
    {
        Sound s=Array.Find(Sounds, s => s.name==name);
        float currentTime=0;
        float start=s.volume;
        while(currentTime<duration)
        {
            currentTime+=Time.deltaTime;
            s.source.volume=Mathf.Lerp(start,0f,currentTime/duration);
            yield return null;

        }
        s.source.Stop();

        yield break;

    }
    public void freeMusic()
    {
        StartCoroutine(FadeOut("level2",1f));
        Play("level2free");
    }

    public IEnumerator changePitch(string name,float duration,float finalPitch)
    {
        Sound s=Array.Find(Sounds, s => s.name==name);
        float currentTime=0;
        float start=s.source.volume;
        float pitchStart=s.source.pitch;
        while(currentTime<duration)
        {

            currentTime+=Time.fixedDeltaTime;
            s.source.pitch=Mathf.Lerp(pitchStart,finalPitch,currentTime/duration);
            yield return null;
        }
        yield break;
    }
}
