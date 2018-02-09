using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour {

    [SerializeField]
    float speed = 5f;
    [SerializeField]
    GameObject Orientator;

    Vector3 forward, right;

	// Use this for initialization
	void Start ()
    {
        forward = Orientator.transform.forward;
        right.y = 0f;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CameraMovement();
	}

    void CameraMovement()
    {
        Vector3 rightMovement = right * speed * Time.deltaTime * Input.GetAxis("HorizontalControl");
        Vector3 forwardMovement = forward * speed * Time.deltaTime * Input.GetAxis("VerticalControl");

        transform.position += rightMovement;
        transform.position += forwardMovement;
    }
}
