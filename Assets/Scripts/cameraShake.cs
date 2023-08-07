using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class cameraShake : MonoBehaviour
{

    public static cameraShake instance;
    private float shakeTimer;
    float startingIntensity;
    float totalTimer;

    CinemachineVirtualCamera virtualCamera;
    public CinemachineVirtualCamera BlueVirtualCamera;
    public CinemachineVirtualCamera PinkVirtualCamera;

    private void Awake()
    {
        instance = this;
        //virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex==1)
        {
            virtualCamera = BlueVirtualCamera;
        }
        if(SceneManager.GetActiveScene().buildIndex>1&&CameraManger.instance.camOn==1)
        {
            virtualCamera = PinkVirtualCamera;
        }
        else if(SceneManager.GetActiveScene().buildIndex > 1 && CameraManger.instance.camOn==0)
        {
            virtualCamera = BlueVirtualCamera;
        }

        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, (1 - (shakeTimer / totalTimer)));
            }
        }
    }

    public void CameraShaking(float intensity,float timer)
    {
        //print(virtualCamera);
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimer = timer;
        totalTimer = timer;
    }
}
