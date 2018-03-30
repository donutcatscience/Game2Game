using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandleMovement : MonoBehaviour {
    public float speed;
    public Text gemText;
    public Text modeText;
    public Rigidbody myRigidBody;
    public int gemCount;

    private string currentMode = "None";
  
    // Use this for initialization
    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody>();
        gemCount = 1;
        gemText.text = "Gem Count: " + gemCount;
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        myRigidBody.AddForce(movement * speed * GetComponent<RunSpeedIncrease>().bonusRunSpeed);

    }

    private void OnTriggerEnter(Collider other)
    {
        //collects gems
        if (other.gameObject.CompareTag("Pick Up"))
        {
            Destroy(other);
            int randomGemValue = Random.Range(10, 25);
            Debug.Log(randomGemValue);
            gemCount += randomGemValue;
        }

    }

    // Update is called once per frame
    void Update () {
        gemText.text = "Gem Count: " + gemCount;
        //handle Mode Text
        if (Input.GetKey(KeyCode.LeftShift)) { currentMode = "Utility"; }
        else if (Input.GetKey(KeyCode.Space)) { currentMode = "Caster"; }
        else{ currentMode = "None";}
        modeText.text = "Current Mode: " + currentMode;
    }
}
