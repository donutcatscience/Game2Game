using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOrit : MonoBehaviour {
    public GameObject Object; //select object to robit
    public float speed; //speed of orbit
    private Vector3 offset;


    // Use this for initialization
    void Start () {
        offset = transform.position - Object.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Object.transform.position + offset;
        OrbitAround(); //makes public object orbit around the attached compnent
	}

    void OrbitAround()
    {
        transform.RotateAround(Object.transform.position, offset, speed * Time.deltaTime);

    }
}
