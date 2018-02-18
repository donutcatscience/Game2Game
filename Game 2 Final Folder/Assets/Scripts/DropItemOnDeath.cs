using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemOnDeath : MonoBehaviour {

    public bool dead;
    public GameObject deathObject;
	// Update is called once per frame
	void Update () {
		if(dead)
        {
            Instantiate(deathObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
	}
}
