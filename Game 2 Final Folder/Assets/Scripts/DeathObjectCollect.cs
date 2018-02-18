using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObjectCollect : MonoBehaviour {

    GameObject[] nearbyUnits;
    float range;
    float tempDistance;
    float step;

	void Start () {
        nearbyUnits = GameObject.FindGameObjectsWithTag("Enemy");
        range = 5f;
        step = 10f * Time.deltaTime;
	}
	
	void Update () {
		foreach(GameObject unit in nearbyUnits)
        {
            if (unit != null)
            {
                tempDistance = Vector3.Distance(transform.position, unit.transform.position);
                if (tempDistance < range)
                {
                    transform.position = Vector3.MoveTowards(transform.position, unit.transform.position, step);
                }
            }
        }
        if (tempDistance < 1f)
        {
            print("I have consumed an item that was dropped on death!");
            Destroy(gameObject);
        }
	}
}
