using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(LoadSceneAsync());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation=SceneManager.LoadSceneAsync(1);
        while(!operation.isDone)
        {
            float progressValue=Mathf.Clamp01(operation.progress/0.9f);
            slider.value=progressValue;
            yield return null;
        }
    }
}
