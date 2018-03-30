using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    GameObject[] units;//array containing all units on the map
    Waypoints[] points;//array containing all waypoints on the map.

	// Use this for initialization
	void Start () {
        units = GameObject.FindGameObjectsWithTag("Enemy");
    }
	
	// Update is called once per frame
	void Update () {
        units = GameObject.FindGameObjectsWithTag("Enemy");//updates to account for changing spawns and death
        foreach (GameObject unit in units)
        {
            //insert desired triggers
        }
    }
}
