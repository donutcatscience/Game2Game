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

        if (nearestUnit == null) tempDistance = Mathf.Infinity;                                                                                         // reset distance to infinity if nearest unit is null
        foreach (GameObject unit in units)                                                                                                                  // loop through all units in scene
        {
            if (unit == null || unit == this.gameObject) continue;                                                                                          // skip the iteration if the unit is null or itself
            if (Vector3.Distance(transform.position, unit.transform.position) < tempDistance)                                                           // if we find a unit that is closer than the others, we obtain their references
            {
                nearestUnit = unit;                                                                                                                         // identify nearest unit
                tempDistance = Vector3.Distance(transform.position, nearestUnit.transform.position);                                                    // set the distance between self & nearest unit       
            }
        }


        if (tempDistance < range)                                                       // if within range,
        {
            transform.position = Vector3.MoveTowards(transform.position, nearestUnit.transform.position, step);    // go towards unit
        }

        if (Vector3.Distance(transform.position, nearestUnit.transform.position) < 1f)  // if close enough to unit
        {
            print("I have consumed an item that was dropped on death!");
            Destroy(gameObject);    // destroy self
        }
	}
}
