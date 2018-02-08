using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour {

    public Transform prefabToSpawn;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                float xpos = transform.position.x + i * 10;
                float zpos = transform.position.z + j * 10;
                Object instanceObj = Instantiate(prefabToSpawn, new Vector3(xpos, .85f, zpos), Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
