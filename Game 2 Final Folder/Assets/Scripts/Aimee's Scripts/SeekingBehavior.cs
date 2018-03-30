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
        arrive(target);

        Vector3.ClampMagnitude(acceleration, maxForce);
        velocity = velocity + acceleration;
        velocity *= Time.deltaTime;
        transform.position += Vector3.ClampMagnitude(velocity, maxSpeed);
	}

    void applyForce(Vector3 force)
    {
        acceleration = force*maxSpeed; 
    }

    void arrive(GameObject target)
    {
        Vector3 VectorToTarget = target.transform.position - transform.position;
        Vector3 desired = new Vector3();

        float d = desired.magnitude;
        desired.Normalize();

        if(d < approximately)
        {
            desired = (VectorToTarget.magnitude / approximately) * (VectorToTarget.normalized * maxSpeed);
        }        
        else
        {
            desired = VectorToTarget * maxSpeed;
        }


        Vector3 steer = desired - velocity;
        steer = Vector3.ClampMagnitude(steer, maxForce);
        applyForce(steer);

    }


}
