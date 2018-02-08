using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandleMovement : MonoBehaviour {
    public float speed;
    public Text gemText;

    private int gemCount = 0;
    private Rigidbody myRigidBody;

    // Use this for initialization
    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody>();
        gemText.text = "Gem Count: " + gemCount;
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        myRigidBody.AddForce(movement * speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            gemCount = gemCount + 1;
            gemText.text = "Score: " + gemCount;
        }

    }

    // Update is called once per frame
    void Update () {

	}
}
