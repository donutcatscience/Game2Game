using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHover : MonoBehaviour {
    public GameObject Object;
    public float speed = 80.0f;
    public float radius = 2.0f;
    public float height = 1.0f;

    private Vector3 offset;
    private Vector3 orbit;

	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate()
    {
        orbit = Object.transform.position + (transform.position - Object.transform.position).normalized * radius;
        orbit.y = height + Object.transform.position.y;
        transform.position = orbit;
        transform.RotateAround(Object.transform.position, new Vector3(0.0f, 1.0f, 0.0f), speed * Time.deltaTime);
        transform.forward = (Object.transform.position - transform.position);
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
    }
}
