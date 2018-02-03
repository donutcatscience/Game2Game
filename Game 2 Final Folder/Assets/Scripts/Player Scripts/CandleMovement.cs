using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleMovement : MonoBehaviour {
    public float speed;
    private Rigidbody myRigidBody;

    // Use this for initialization
    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody>();

	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        myRigidBody.AddForce(movement * speed);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
