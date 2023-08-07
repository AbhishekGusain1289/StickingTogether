using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastColorChanger : MonoBehaviour
{
    //public GameObject[] Floor;
    //[SerializeField] Color FloorColor;
    [SerializeField] float transitionTime;
    public GameObject Background;
    [SerializeField] Color BackgroundColor;
    [SerializeField] Color firstColor;
    [SerializeField] Color SecondColor;
    public ParticleSystem currentParticleSystem;
    ParticleSystem.MainModule main;





    // Start is called before the first frame update
    void Start()
    {   
        main = currentParticleSystem.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pinkPlayer.inJail)
        {
            ColorChanger();
            
        }
    }

    public void ColorChanger()
    {
        main.startColor = new ParticleSystem.MinMaxGradient(firstColor, SecondColor);


        //foreach (GameObject floor in Floor)
        //{
        //    floor.GetComponent<SpriteRenderer>().color = Color.Lerp(floor.GetComponent<SpriteRenderer>().color, FloorColor, transitionTime);
        //}
        Background.GetComponent<SpriteRenderer>().color = Color.Lerp(Background.GetComponent<SpriteRenderer>().color, BackgroundColor, transitionTime);

    }
}
