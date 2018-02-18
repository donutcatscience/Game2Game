using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class PlayerController : MonoBehaviour {

    //Camera cam;  // get reference to the main camera
    [SerializeField] LayerMask movementMask; // reference to Ground layer; set in the inspector
    NavMeshAgent agent; // get reference to the agent; used for navigation
    GameObject[] enemies; // array of all enemies in scene
    float distanceToEnemy; // distance from this enemy to nearest enemy
    float shortestDistance; // shortest distance between this enemy & other enemies
    GameObject nearestEnemy = null; // we will be finding the nearest enemy
    float aggroRange = 0.5f; // the distance in which the enemy will target nearby enemies

    //int raycastRange = 1000; // range of raycast for click

    private Transform target; // targets for waypoints
    private int waypointIndex = 0; // index for which waypoint to travel to

    void Start () {
        //cam = Camera.main; // obtain camera reference
        agent = GetComponent<NavMeshAgent>(); // obtain agent

        target = Waypoints.points[waypointIndex]; // obtain array of waypoints
        enemies = GameObject.FindGameObjectsWithTag("Enemy"); // identify enemies in scene; MUST PUT THIS IN UPDATE IF ENEMIES SPAWN DURING GAMEPLAY
    }
	
	void Update () {
        #region left click to move player (TEST)
        /*
		if(Input.GetMouseButtonDown(0)) // On left mouse button press
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // cast a ray where the user clicked
            RaycastHit hit; // obtain information on what was clicked

            if (Physics.Raycast(ray, out hit, raycastRange, movementMask)) // if the mouse clicked on the Ground layer, move the agent to the point
            {
                MoveToPoint(hit.point); // calls a function to move agent
            }
        }
        */
        #endregion

        //MoveToPoint(target.position); // agent will move to waypoint

        if(Vector3.Distance(transform.position, target.position) <= 0.2f) // if agent has reached close enough to waypoint
        {
            GetNextWaypoint(); // go to next waypoint
        }

        #region track enemies
        shortestDistance = Mathf.Infinity; // reset distance between enemy & nearest other enemy
        nearestEnemy = null;
        foreach (GameObject enemy in enemies) // loop through all enemies in scene
        {
            if (this.gameObject != enemy  && enemy != null) // exclude this enemy from the loop, since this is also an enemy & we don't want this to target itself
            {
                distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // calculate distances between this enemy & other enemies
                if (distanceToEnemy < shortestDistance) // have we found an enemy that is closer than the others?
                {
                    shortestDistance = distanceToEnemy; // set shortest distance
                    nearestEnemy = enemy; // set nearest enemy
                }
            }
        }
        if(nearestEnemy != null && shortestDistance <= aggroRange) // if there is an enemy within range
        {
            MoveToPoint(nearestEnemy.transform.position); // move towards the nearest enemy
            //print("near an enemy!");
        }
        else MoveToPoint(target.position); // agent will move towarsds waypoint
        #endregion

    }

    void GetNextWaypoint()
    {
        waypointIndex++; // set index to next waypoint
        if(waypointIndex >= Waypoints.points.Length) // check if index reaches out of bounds
        {
            waypointIndex = 0; // set it back to the beginning
        }
        target = Waypoints.points[waypointIndex]; // set the waypoint
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
}
