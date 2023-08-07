using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance { get; private set; }
    [SerializeField] GameObject Instruct;
    [SerializeField] GameObject Shoot;

    public bool TutorialPlaying=false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Instruct.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Instructions();
    }

    void Instructions()
    {
        if(SceneManager.GetActiveScene().buildIndex==1)
        {
            level1();
        }
        else if(SceneManager.GetActiveScene().buildIndex==2)
        {
            level2();
        }
    }

    private void level2()
    {
        if(pinkPlayer.inJail)
        {
            Instruct.SetActive(false);
        }
        else if(!pinkPlayer.inJail)
        {
            StartCoroutine(waiting());

        }

    }

    void level1()
    {
        DialogueManager.instance.dialoguePlaying = true;
        Instruct.SetActive(true);
        Shoot.SetActive(false);
        
    }

    public void DestroyMe()
    {
        Shoot.SetActive(true);
        DialogueManager.instance.dialoguePlaying = false;
        Destroy(gameObject);
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(5f);
        Instruct.SetActive(true);
        Shoot.SetActive(false);
        DialogueManager.instance.dialoguePlaying = true;
    }
}
