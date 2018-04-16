using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float smoothFactor = 5f;

    Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        offset = target.position + new Vector3(-7.0f, 15.0f, -7.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothFactor * Time.deltaTime);
	}
}
