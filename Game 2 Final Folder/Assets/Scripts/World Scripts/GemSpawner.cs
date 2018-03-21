using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour {

    public Transform prefabToSpawn;
    public bool elevatedTerrain;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if (elevatedTerrain)
                {
                    //Spawns recources correctly above different elevated terrain
                    float xpos = transform.position.x + i * 10;
                    float zpos = transform.position.z + j * 10;
                    float ypos = Terrain.activeTerrain.SampleHeight(new Vector3(xpos, 0, zpos)) + 0.85f;
                    Object instanceObj = Instantiate(prefabToSpawn, new Vector3(xpos, ypos, zpos), Quaternion.identity);
                } else
                {
                    float xpos = transform.position.x + i * 10;
                    float zpos = transform.position.z + j * 10;
                    Object instanceObj = Instantiate(prefabToSpawn, new Vector3(xpos, .85f, zpos), Quaternion.identity);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
