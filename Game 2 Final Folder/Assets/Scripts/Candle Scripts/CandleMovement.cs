using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandleMovement : MonoBehaviour {
    public float speed;
    public Text gemText;
    public Text modeText;
    public Rigidbody myRigidBody;

    private string currentMode = "None";
    private int gemCount = 0;
  
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
        //collects gems
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            gemCount = gemCount + 1;
            gemText.text = "Gem Count: " + gemCount;
        }

    }

    // Update is called once per frame
    void Update () {
        
        //handle Mode Text
        if (Input.GetKey(KeyCode.LeftShift)) { currentMode = "Utility"; }
        else if (Input.GetKey(KeyCode.Space)) { currentMode = "Caster"; }
        else{ currentMode = "None";}
        modeText.text = "Current Mode: " + currentMode;
    }
}
