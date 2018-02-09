using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickable : MonoBehaviour {

    bool open = false;

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
}
