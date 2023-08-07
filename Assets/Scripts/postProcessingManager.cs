using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class postProcessingManager : MonoBehaviour
{
    public static postProcessingManager instance;


    public Volume volume;
    ChromaticAberration chromatic;
    Vignette vignette;
    LensDistortion lensDist;
    float lensDistIntens=0.25f;
    [SerializeField] float intensity=0.3f;
    [SerializeField] float IntensityA=0.5f;
    [SerializeField] float IntensityB = 0.3f;
    [SerializeField] float startSpeed;
    [SerializeField] float endSpeed;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        volume.profile.TryGet<ChromaticAberration>(out chromatic);
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<LensDistortion>(out lensDist);
    }

    // Update is called once per frame
    void Update()
    {
        gettingHurt();
    }

    private void gettinHurt2()
    {
        intensity = IntensityB;
        lensDistIntens=0.25f;
        chromatic.intensity.value = Mathf.Lerp(chromatic.intensity.value, intensity, Time.deltaTime*endSpeed);
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, intensity, Time.deltaTime * endSpeed);
        lensDist.intensity.value=Mathf.Lerp(lensDist.intensity.value, lensDistIntens, Time.deltaTime*startSpeed);
    }
    public void gettingHurt()
    {
        
        if (Input.GetKeyDown(KeyCode.T)||(BluePlayer.instance.HitPlayer))
        {
            
            intensity = IntensityA;
            lensDistIntens=0.5f;
        }
        if(SceneManager.GetActiveScene().buildIndex > 1 && CameraManger.instance.camOn==1)
            if(pinkPlayer.instance.HitPinkPlayer)
                intensity = IntensityA;
        chromatic.intensity.value = Mathf.Lerp(chromatic.intensity.value, intensity-0.1f, Time.deltaTime*startSpeed);
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, intensity, Time.deltaTime*startSpeed);
        lensDist.intensity.value=Mathf.Lerp(lensDist.intensity.value, lensDistIntens, Time.deltaTime*startSpeed);
        if(vignette.intensity.value>(IntensityA-0.08f))
        {
            StartCoroutine(waiting());
            gettinHurt2();
        }
        else
            return;
        

    }
    
    

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(2f);
    }

}
