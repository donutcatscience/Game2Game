using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealOverTime : MonoBehaviour {
    public Text healthText;

    //following values to be adjusted later
    public int maxHealth = 100;
    public int tickInterval = 60;
    public int healPerTick = 5;

    private int currentHealth = 50; //placeholder value to be removed later
    private float frameCounter; //counts frames to find out when to tick HP

	// Use this for initialization
	void Start () {
        healthText.text = "Health: " + currentHealth + " / " + maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (frameCounter >= tickInterval && currentHealth < maxHealth)
            {
                currentHealth = healPerTick + currentHealth;
                frameCounter = 0;
            }
            else if (currentHealth < maxHealth)
            {
                ++frameCounter;
            }
        }
        healthText.text = "Health: " + currentHealth + " / " + maxHealth;
    }
}
