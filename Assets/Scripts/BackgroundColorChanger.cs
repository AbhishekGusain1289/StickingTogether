using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{

    public GameObject[] Floor;
    [SerializeField] Color FloorColor;
    [SerializeField] float transitionTime;
    public GameObject Background;
    [SerializeField] Color BackgroundColor;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!pinkPlayer.inJail)
        {
            ColorChanger();

        }
        
    }

    public void ColorChanger()
    {
        foreach (GameObject floor in Floor)
        {
            floor.GetComponent<SpriteRenderer>().color = Color.Lerp(floor.GetComponent<SpriteRenderer>().color,FloorColor,transitionTime);
        }
        Background.GetComponent<SpriteRenderer>().color = Color.Lerp(Background.GetComponent<SpriteRenderer>().color, BackgroundColor, transitionTime);
        
    }
}
