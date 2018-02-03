using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject PlayerSphere;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - PlayerSphere.transform.position;
	}



    // Update is called once per frame
    void Update () {
		
	}

    private void LateUpdate()
    {
        transform.position = PlayerSphere.transform.position + offset;
    }
}
