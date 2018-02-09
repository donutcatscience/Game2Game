using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Original Author of this code can be found from this YouTube link:
//https://www.youtube.com/watch?v=XhliRnzJe5g&t=136s

public class PlayerMovementController : MonoBehaviour {

    [SerializeField]
    float speed = 5f;
    public GameObject Orientator;
    Vector3 north, west;

	// Use this for initialization
	void Start ()
    {
        //set north vector to forward vector based on camera position
        north = Orientator.transform.forward;
        north.y = 0f;

        //keeps direction constant for vector, but normalizes length to that of either 1 or zero
        north = Vector3.Normalize(north);

        //creates a rotation around x axis multiplied with normalized north
        //sets object to use new isometric axis format
        west = Quaternion.Euler(new Vector3(0, 90, 0)) * north;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.anyKey)
        {
            PlayerAction();
        }
	}

    void PlayerAction()
    {
        //calculates a new direction for the now isometric oriented vector based on which keys are pressed
        //"HorizontaControl" When 'D' is pressed, resulting value is '1', 'A' is '-1'
        //"VerticalControl" When 'W' is pressed, resulting value is '1', 'S' is '-1'
        //No input or any other input that is not 'WASD' results in the Input.GetAxis() to return a zero direction
        //NOTE: This line of code is an artifact left by the original author that this code had originated from

        /*
        Vector3 isoDirection = new Vector3(Input.GetAxis("HorizontalControl"), 0, Input.GetAxis("VerticalControl"));
        */

        //net result of these calculated vectors is that we use them in order to direct the object's movement
        //with speed and time factored into the formula
        Vector3 westMovement = west * speed * Time.deltaTime * Input.GetAxis("HorizontalControl");
        Vector3 northMovement = north * speed * Time.deltaTime * Input.GetAxis("VerticalControl");

        //This line of code normalizes the combined vectors into one normalized vector that controls direction
        Vector3 combinedVector = Vector3.Normalize(westMovement + northMovement);

        //Rotate object to move in the given direction of input
        transform.forward = combinedVector;

        //apply force vectors to object
        transform.position += westMovement;
        transform.position += northMovement;
    }
}
