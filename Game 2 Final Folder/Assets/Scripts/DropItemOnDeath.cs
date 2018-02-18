using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemOnDeath : MonoBehaviour {

    bool dead = false;
    public GameObject deathObject;



    private void OnMouseDown()
    {
        dead = true;
    }

    void Update () {
		if(dead)
        {
            Instantiate(deathObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
	}
}
