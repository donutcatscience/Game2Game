using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunSpeedIncrease : MonoBehaviour {
    public Text runSpeedText;

    //following values to be adjusted later
    public int runSpeedBonus = 5;

    private float currentRunSpeed;
	// Use this for initialization
	void Start () {
        currentRunSpeed = GetComponent<CandleMovement>().speed;
        runSpeedText.text = "Current Run Speed: " + currentRunSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentRunSpeed = GetComponent<CandleMovement>().speed + runSpeedBonus;
        }
        else
        {
            currentRunSpeed = GetComponent<CandleMovement>().speed;
        }
        runSpeedText.text = "Current Run Speed: " + currentRunSpeed;
    }
}
