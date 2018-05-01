using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour 
{
    private double angle;
    private bool active;
    private ParticleSystem ps;

	// Use this for initialization
	void Start ()
    {
        angle = 60;
        active = false;
        ps = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(active && angle > 0)
        {
            angle -= 0.1;
        }

        if(active && angle <= 0)
        {
            active = false;
            angle = 60;
        }

        if(!active && angle == 60)
        {
            ps.Stop();
        }
	}
}
