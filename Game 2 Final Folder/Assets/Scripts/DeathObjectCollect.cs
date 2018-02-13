using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObjectCollect : MonoBehaviour {

    GameObject[] units;                                           // find nearest unit
    float range;                                                        // range at which object will attract to unit
    float tempDistance;                                                 // calculate the distance to unit
    float step;                                                         // speed
    GameObject nearestUnit;

	void Start () {
        units = ListOfUnits.units;       // find array of all units
        range = 5f;                                                     // set range
        step = 10f * Time.deltaTime;                                    // set speed
        nearestUnit = null;
	}
	
	void Update () {
        
        foreach (GameObject unit in units)                         // for each unit
        {
            if (unit != null)                                           // if the unit exists
            {
                nearestUnit = unit;
                tempDistance = Vector3.Distance(transform.position, unit.transform.position);   // calculate distance between self & unit
                if (tempDistance < range)                                                       // if within range,
                {
                    transform.position = Vector3.MoveTowards(transform.position, unit.transform.position, step);    // go towards unit
                }
            }
        }
        
        if (Vector3.Distance(transform.position, nearestUnit.transform.position) < 1f)  // if close enough to unit
        {
            print("I have consumed an item that was dropped on death!");
            Destroy(gameObject);    // destroy self
        }
	}
}
