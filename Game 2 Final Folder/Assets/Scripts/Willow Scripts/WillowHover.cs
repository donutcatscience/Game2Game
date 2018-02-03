using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillowHover : MonoBehaviour {
    public GameObject CandleBody;
    private Vector3 offset;


    // Use this for initialization
    void Start () {
        offset = transform.position - CandleBody.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

        private void LateUpdate()
    {
        transform.position = CandleBody.transform.position + offset;
    }
}
