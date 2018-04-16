using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleAnimationHandler : MonoBehaviour {

    Animator candleAnimator;
    bool isMovingPressed = false, isUsingAbilityPressed = false, isHit = false;

	void Start ()
    {
        candleAnimator = GetComponent<Animator>();
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) //check for any movement keys pressed
        {
            isMovingPressed = true;
        }
        else
        {
            isMovingPressed = false;
        }

        if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)) //check for any action keys pressed
        {
            isUsingAbilityPressed = true;
        }
        else
        {
            isUsingAbilityPressed = false;
        }

        candleAnimator.SetBool("isMoving", isMovingPressed);    //send to animator component on which keys are pressed
        candleAnimator.SetBool("isUsingAbility", isUsingAbilityPressed);

        isHit = false;
	}

    private void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.CompareTag("Enemy Projectile"))
        {
            {
                isHit = true;
            }
        }
    }
}
