using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    public BaseVariables NPC;

    // make a reference to a script that has all of the functions
    public Functions functions;

    public virtual void Start()
    {
        if(NPC == null) NPC = gameObject.transform.root.gameObject.GetComponent<BaseVariables>();
        if(functions == null) functions = NPC.GetComponent<Functions>();
    }


}
