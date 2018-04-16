using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add this script to whatever you need for spawning vfx

public class DamageSparkSpawn : MonoBehaviour
{
    public bool isHit = false;
    public GameObject prefabToSpawn;

	void Update ()
    {
		if(isHit)
        {
            Instantiate(prefabToSpawn, transform.position, gameObject.transform.rotation);
            isHit = false;
        }
	}
}
