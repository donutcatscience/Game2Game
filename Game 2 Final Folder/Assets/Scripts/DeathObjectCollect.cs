using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObjectCollect : MonoBehaviour
{

    GameObject[] units;                                                 // find nearest unit
    float range;                                                        // range at which object will attract to unit
    float tempDistance;                                                 // calculate the distance to unit
    float step;                                                         // speed
    GameObject nearestUnit;                                             // nearest unit

	void Start ()
    {
        units = ListOfUnits.units;                                      // find array of all units
        range = 5f;                                                     // set range
        step = 10f * Time.deltaTime;                                    // set speed
        nearestUnit = null;                                             // set nearby unit
        
	}
	
	void Update ()
    {
        tempDistance = Mathf.Infinity;                              // reset distance
        foreach (GameObject unit in units)                          // for each unit
        {
            if (unit == null) continue;                                       // if the unit does not exist, continue through the iteration
            if (Vector3.Distance(transform.position, unit.transform.position) < tempDistance)                                                       // if within range,
            {
                nearestUnit = unit;                                                                                 // identify nearest unit
                tempDistance = Vector3.Distance(transform.position, nearestUnit.transform.position);                // set distance between self & nearest unit
            }
            
        }
        if (tempDistance < 1f)                                              // if close enough to unit
        {
            print("I have consumed an item that was dropped on death!");
            Destroy(gameObject);                                            // destroy self
        }
        else if (tempDistance < range) transform.position = Vector3.MoveTowards(transform.position, nearestUnit.transform.position, step);    // go towards unit
       
	}
}
