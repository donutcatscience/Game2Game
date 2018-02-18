using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunSpeedIncrease : MonoBehaviour {
    public Text runSpeedText;

    //following values to be adjusted later
    public float runSpeedBonus = 1.5F;
    public float bonusRunSpeed;
	// Use this for initialization
	void Start () {
        runSpeedText.text = "Current Run Speed: " + GetComponent<PlayerMovement>().speed * bonusRunSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            bonusRunSpeed = runSpeedBonus;
        }
        else
        {
            bonusRunSpeed = 1;
        }
        runSpeedText.text = "Current Run Speed: " + GetComponent<PlayerMovement>().speed * bonusRunSpeed;
    }
}
