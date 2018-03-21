using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickable : MonoBehaviour {

    bool open = false;
    Renderer rend;
    Color originalColor;
    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }
    void OnMouseDown()
    {
        if(open)
        {
            this.transform.Translate(0f,-5f,0f);
        }
        else
        {
            this.transform.Translate(0f, 5f, 0f);
        }
        open = !open;
    }
    void OnMouseOver()
    {
        rend.material.color = Color.green;
    }
    void OnMouseExit()
    {
        rend.material.color = originalColor;        
    }
}
