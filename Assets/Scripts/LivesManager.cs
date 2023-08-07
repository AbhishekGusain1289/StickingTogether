using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;
    int lives=3;
    [SerializeField]
    GameObject[] lifes;

    [SerializeField]
    private GameObject lifeBar;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        LifeShowing();
    }
    public void LifeShowing()
    {
        if((!UIManager.paused)&&(!DialogueManager.instance.dialoguePlaying))
        {
            lifeBar.SetActive(true);
        }
        else
            lifeBar.SetActive(false);
    }
    
    public void HitChecker()
    {
        if(lives>0)
        {

            lifes[lives-1].gameObject.SetActive(false);
            lives--;
        }
        if(lives==0)
        {

            StartCoroutine( DyingParticles());

        }
    }

    public IEnumerator DyingParticles()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

            BluePlayer.instance.Dying();
            yield return new WaitForSeconds(0.5f);
            BluePlayer.instance.DyingFinally();
            StartCoroutine(LevelLoader.instance.LoadNextLevel(SceneManager.GetActiveScene().buildIndex));
        }
        else if(SceneManager.GetActiveScene().buildIndex >1)
        {
            if (CameraManger.instance.camOn == 0)
            {
                BluePlayer.instance.Dying();
                yield return new WaitForSeconds(0.5f);
                BluePlayer.instance.DyingFinally();
                StartCoroutine(LevelLoader.instance.LoadNextLevel(SceneManager.GetActiveScene().buildIndex));
            }
            else if (CameraManger.instance.camOn==1)
            {
                pinkPlayer.instance.Dying();
                yield return new WaitForSeconds(0.5f);
                pinkPlayer.instance.DyingFinally();
                StartCoroutine(LevelLoader.instance.LoadNextLevel(SceneManager.GetActiveScene().buildIndex));
            }
        }
    }

}
