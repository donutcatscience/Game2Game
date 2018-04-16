using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillowAnimationHandler : MonoBehaviour {

    bool isCasterMode = false;
    bool isUsingBasicAttack = false, smallBomb = false, bigBomb = false;

    private Animator willowAnimator;

    public GameObject willowOrb;
    private MeshRenderer rend;

	// Use this for initialization
	void Start ()
    {
        willowAnimator = GetComponent<Animator>();

        rend = willowOrb.GetComponent<MeshRenderer>();
        rend.enabled = true;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(Input.GetKey(KeyCode.Space))
        {
            isCasterMode = true;
        }
        else
        {
            isCasterMode = false;
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(isCasterMode)
            {
                smallBomb = true;
            }
            else
            {
                rend.enabled = false;
            }
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (isCasterMode)
            {
                bigBomb = true;
            }
            else
            {
                rend.enabled = false;
            }
        }

        if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
        {
            smallBomb = false;
            bigBomb = false;
            rend.enabled = true;
        }

        willowAnimator.SetBool("smallBomb", smallBomb);    //send to animator component on which keys are pressed
        willowAnimator.SetBool("bigBomb", bigBomb);
    }
}
