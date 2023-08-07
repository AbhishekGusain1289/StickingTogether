using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private PlayerControls Controller;
    public static bool paused = false;

    [SerializeField] GameObject PauseMenu;
    [SerializeField] public GameObject ControllMenu;
    
    [SerializeField]
    public GameObject Shoot;


    // Start is called before the first frame update
    void Start()
    {
        instance=this;
    }
    private void Awake()
    {
        ControllMenu.SetActive(false);
        Controller = new PlayerControls();
        if(SceneManager.GetActiveScene().buildIndex>0)
        {
            PauseMenu.SetActive(false);
            if(Application.isMobilePlatform)
            ControllMenu.SetActive(true);
            else
            {
                Shoot.SetActive(true);
            }

        }
        if(Application.isMobilePlatform)
        Shoot.SetActive(false);

    }

    // Update is called once per frame
    private void OnEnable()
    {

        Controller.Pause.PauseMenu.performed += Pausing;
        Controller.Pause.Enable();
    }
    private void OnDisable()
    {
        Controller.Pause.PauseMenu.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ControlHider();
        
    }

    void Pausing(InputAction.CallbackContext obj)
    {
        if(!DialogueManager.instance.dialoguePlaying)
        {

        
            if (obj.performed)
            {

                if(!paused)
                {
                    Pause();
                }
                else if(paused)
                {
                    Resume();
                }
            }
        }
       
        
    }


    public void Pause()
    {
        int level=SceneManager.GetActiveScene().buildIndex;
        string stage="level"+level;
        StartCoroutine(AudioManager.instance.changePitch(stage,0.3f,0.4f));

        Time.timeScale = 1f;
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Resume()
    {
        int level=SceneManager.GetActiveScene().buildIndex;
        string stage="level"+level;
        StartCoroutine(AudioManager.instance.changePitch(stage,0.3f,1f));
        
        Time.timeScale = 1f;
        paused = false;
        PauseMenu.SetActive(false);
        
    }

    public void Reload()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        string stage="level"+level;
        Time.timeScale = 1f;
        StartCoroutine(AudioManager.instance.FadeOut(stage,1f));
        if(level==2)
        {
            pinkPlayer.inJail=true;
        }
        StartCoroutine(LevelLoader.instance.LoadNextLevel(level));
        paused=false;
    }

    public void MainMenu()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        string stage="level"+level;
        Time.timeScale = 1f;
        pinkPlayer.inJail=true;
        StartCoroutine(AudioManager.instance.FadeOut(stage,1f));
        StartCoroutine(LevelLoader.instance.LoadNextLevel(0));
        paused=false;
        if(Application.isMobilePlatform)
        UIManager.instance.ControllMenu.SetActive(true);
        else
        UIManager.instance.Shoot.SetActive(true);
    }

    void ControlHider()
    {
        if(DialogueManager.instance.dialoguePlaying||paused)
        {
            if(Application.isMobilePlatform)
            {
            ControllMenu.SetActive(false);
            

            }
            else
            Shoot.SetActive(false);
        }
        else
        {
            if(Application.isMobilePlatform)
            {
                ControllMenu.SetActive(true);
                
            }
            else
            Shoot.SetActive(true);
        }
    }


}
