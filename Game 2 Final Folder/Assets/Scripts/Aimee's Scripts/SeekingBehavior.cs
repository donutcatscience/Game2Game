using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingBehavior : MonoBehaviour
{
    //Declaring variables

    //Object we'll be seeking
    public GameObject target;
    //Maximum speed we'll be travelling to get to our desired velocity
    public float maxSpeed;
    //How good we'll be at steering control
    //High number = super controlled, low number = super slow/clumsy
    public float maxForce;
    //The distance where it'll start to slow down in relation to where the target is
    public float approximately;

    public Vector3 velocity;
    public Vector3 acceleration;


	// Use this for initialization
	void Start ()
    {
        acceleration = new Vector3(0, 0, 0);
        velocity = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3.ClampMagnitude(acceleration, maxForce);
        velocity = velocity + acceleration;
        velocity *= Time.deltaTime;
        //transform.position = Vector3.ClampMagnitude(velocity, maxSpeed); //this trying set the position directly, instead of moving
        transform.position += Vector3.ClampMagnitude(velocity, maxSpeed);
       // acceleration = acceleration * 0.0f;
        arrive(target);
	}

    void applyForce(Vector3 force)
    {
        acceleration = acceleration + force;
    }

    void arrive(GameObject target)
    {
        Vector3 desired = target.transform.position - transform.position;

        //desired.Normalize();
        //desired = desired * maxSpeed;

        float d = desired.magnitude;
        desired.Normalize();

        if(d < approximately)
        {
            float m = Map(0, maxSpeed, 0, approximately, d);
            desired = desired * m;
        }
        
        else
        {
            desired = desired * maxSpeed;
        }


        Vector3 steer = desired - velocity;
        steer = Vector3.ClampMagnitude(steer, maxForce);
        applyForce(steer);

    }

    public float Map(float from, float to, float from2, float to2, float value)
    {
        if(value <= from2)
        {
            return from;
        }

        else if (value >= to2)
        {
            return to;
        }

        else
        {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }

}
