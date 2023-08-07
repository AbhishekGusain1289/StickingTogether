using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public static LevelLoader instance;


    public Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator LoadNextLevel(int levelIndex)
    {
        switch (levelIndex-1) {
            case 0:
                StartCoroutine(AudioManager.instance.FadeOut("menu", 1f));
                break;
            case 1:
                StartCoroutine(AudioManager.instance.FadeOut("level1", 1f));
                break;
            case 2:
                StartCoroutine(AudioManager.instance.FadeOut("level2", 1f));
                break;
            case 3:
                StartCoroutine(AudioManager.instance.FadeOut("level3", 1f));
                break;
            case 4:
                StartCoroutine(AudioManager.instance.FadeOut("level4", 1f));
                break;
            case 5:
                StartCoroutine(AudioManager.instance.FadeOut("level5", 1f));
                break;
            case 6:
                StartCoroutine(AudioManager.instance.FadeOut("level6", 1f));
                break;

        }

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        if(levelIndex==7)
        {
            levelIndex = 0;
        }
        AsyncOperation operation= SceneManager.LoadSceneAsync(levelIndex);
        while(!operation.isDone)
        yield return null;
    }
    
}
