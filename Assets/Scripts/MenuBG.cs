using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBG : MonoBehaviour
{

    Vector2 StartPos;
    [SerializeField] float moveModifier;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos=Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float x = Mathf.Lerp(transform.position.x, StartPos.x* moveModifier, 2f * Time.deltaTime);
        float y = Mathf.Lerp(transform.position.y, StartPos.y* moveModifier, 2f * Time.deltaTime);

        transform.position = new Vector2(pos.x, pos.y);
    }
}
