using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHover : MonoBehaviour {
    public GameObject Object;
    private Vector3 offset;


    // Use this for initialization
    void Start () {
        offset = transform.position - Object.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

        private void LateUpdate()
    {
        transform.position = Object.transform.position + offset;
    }
}
