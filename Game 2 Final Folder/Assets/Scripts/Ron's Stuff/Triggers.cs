﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    public BaseVariables NPC;

    public virtual void Start()
    {
        if(NPC == null) NPC = gameObject.transform.root.gameObject.GetComponent<BaseVariables>();
    }


}
