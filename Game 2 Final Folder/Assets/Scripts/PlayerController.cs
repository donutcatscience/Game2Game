using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[RequireComponent(typeof(NavMeshAgent))]

public class PlayerController : MonoBehaviour {

    #region variables

    public Targeting targeting;                 // used for switching of states
    public WaypointDirection waypointDirection; // used for determining direction of waypoints

    NavMeshAgent agent;                     // get reference to the agent; used for navigation

    NavMeshPath primaryPath;                // stores primary path; the main path the unit will be following
    NavMeshPath secondaryPath;              // stores secondary path
    NavMeshPath tempPath;                   // used in FindNearest function to temporarily calculate distance of path

    public  float aggroRange;               // the distance in which the unit will target nearby enemy units
    public float waypointRange;
    public  float primaryPathDist;          // stores the distance of the primary path
    public  float secondaryPathDist;        // stores the distance of the secondary path
    public  float distToNearestUnit;        // shortest distance between this enemy & other enemies
            float distToNearestWaypoint;    // not used; stores the distance that FindNearest function returns
    
            int waypointIndex;              // traverses array of units in scene
            int waypointDir;                // direction of traversal
            
            GameObject waypointTarget;      // stores the waypoint that the path will target
    public  GameObject primaryTarget;       // target for primary path
    public  GameObject secondaryTarget;     // target for secondary path
            GameObject nearestWaypoint;     // stores the nearest waypoint
            GameObject[] units;             // array of all units in scene
    public  GameObject nearestUnit;         // stores the nearest unit
            GameObject nearestObj;          // used in FindNearest function to temporarily store reference to nearest object

    #endregion

    #region Enumerators

    public enum Targeting                   // Mini-state machine that determines whether the unit is targeting waypoints or units
    {
        units,
        waypoints
    };

    public enum WaypointDirection           // Should the order of waypoints be ping-ponged or looped?
    {
        pingpong,
        loop
    };

    #endregion

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();                           // obtain agent
        tempPath = new NavMeshPath();                                   // create path
        primaryPath = new NavMeshPath();                                // create path
        secondaryPath = new NavMeshPath();                              // create path
        waypointIndex = 0;                                              // set position in array of waypoints
        waypointTarget = Waypoints.points[waypointIndex].gameObject;    // set target to first waypoint in array
        
        units = ListOfUnits.units;                                      // identify enemies in scene; MUST PUT THIS IN UPDATE WHEN ENEMIES SPAWN DURING GAMEPLAY
        targeting = Targeting.waypoints;                                // set state to target waypoints by default
        waypointDir = 1;                                                // waypoint traversal proceeds forward
        distToNearestWaypoint = 0f;
    }

    void Update()
    {
        FindNearest(units, out nearestUnit, out distToNearestUnit);                                                                 // find the nearest unit

        switch (targeting)                                                                                                          // mini state machine: follow waypoints  <-> follow units
        {
            #region Target Units
            case Targeting.units:                                                                                                   // Target Units:
                primaryTarget = nearestUnit;                                                                                            // primary path targets nearest unit
                secondaryTarget = waypointTarget;                                                                                       // secondary path targets waypoints
                
                if(distToNearestUnit > aggroRange || primaryTarget == null)                                                              // if the targeted unit has died
                {
                    targeting = Targeting.waypoints;                                                                                    // target waypointa
                    FindNearest(Waypoints.points, out nearestWaypoint, out distToNearestWaypoint);                                      // find nearest waypoint
                    waypointTarget = nearestWaypoint;                                                                                   // target nearest waypoint
                    waypointIndex = ArrayUtility.IndexOf(Waypoints.points, nearestWaypoint);                                            // set the index of waypoint array
                }
            break;
            #endregion
            
            #region Target Waypoints
            case Targeting.waypoints:                                                                                                           // Target Waypoints
                primaryTarget = waypointTarget;                                                                                                     // primary path targets waypoints
                secondaryTarget = nearestUnit;                                                                                                      // secondary path targets nearest unit

                if (Vector3.Distance(transform.position, Waypoints.points[waypointIndex].transform.position) < waypointRange) GetNextWaypoint();    // get the next waypoint if current waypoint is reached within distance

                if (distToNearestUnit <= aggroRange)                                                                                                // if unit is within range
                {
                    targeting = Targeting.units;                                                                                                    // target nearest enemy
                }
            break;
            #endregion
        }

        if (CalcPath(primaryPath, primaryTarget))                                                                                                   // calculate primary path distance
        {
            primaryPathDist = PathDistance(primaryPath, Color.red, true); 
            agent.SetPath(primaryPath);
        }
        else                                                                                                                                        // otherwise, path is invalid.  Reset properties & find next waypoint
        {
            primaryPathDist = Mathf.Infinity;
            agent.ResetPath();
            GetNextWaypoint();                                                                                                                      // WARNING: this function may be moved elsewhere
        }        

        if (CalcPath(secondaryPath, secondaryTarget))                                                                                               // calculate secondary path distance
        {
            secondaryPathDist = PathDistance(secondaryPath, Color.blue, true);                                                              
        }
    }

    #region functions

    float PathDistance(NavMeshPath path, Color color, bool showPath)                // returns the distance of the path & draws path in editor
    {                                                                                   //
        float pathDist = 0f;                                                            // reset distance
        for (int i = 0; i < path.corners.Length - 1; i++)                               // calculate for every corner of the path
        {                                                                               //
            if(showPath) Debug.DrawLine(path.corners[i], path.corners[i + 1], color);   // draw line from current corner to next corner
            pathDist += Vector3.Distance(path.corners[i], path.corners[i + 1]);         // accumulate distance from current corner to next
        }                                                                               //
        return pathDist;                                                                // return total distance of path
    }

    bool CalcPath(NavMeshPath path, GameObject target)                  // Calculates a path from self to a target destination; returns true if path is uninterrupted
    {                                                                       //
        if (target == null)                                                 // if our target does not exist, reset our path & return false
        {                                                                   //
            agent.CalculatePath(transform.position, path);                  //
            return false;                                                   //
        }                                                                   //
        agent.CalculatePath(target.transform.position, path);               // calculates the path
        if (path.status == NavMeshPathStatus.PathPartial) return false;     // return false if interrupted
        else return true;                                                   // return true if intact
    }

    void GetNextWaypoint()                                              // Cycles through array of waypoints                        // OUT OF BOUNDS ERROR
    {                                                                       //
        waypointIndex += waypointDir;                                       // iterate to next waypoint in array
        if (waypointIndex >= Waypoints.points.Length || waypointIndex < 0)  // check if index reaches out of bounds
        {                                                                   //
            switch (waypointDirection)                                      // check if the waypoint loops or ping-pongs
            {                                                               //
                case WaypointDirection.pingpong:                        // ping-pong
                    waypointDir = -waypointDir;                             // reverse direction when the end is reached
                    waypointIndex += waypointDir;                           // iterate once more to get back in bounds
                break;                                                      //
                case WaypointDirection.loop:                            // loop
                    waypointIndex = 0;                                      // reset iterator back to beginning
                break;                                                      //
            }                                                               //
        }                                                                   //
        waypointTarget = Waypoints.points[waypointIndex].gameObject;        // set the waypoint
    }

    void FindNearest(GameObject[] array, out GameObject nearestObj, out float shortDistance)    // Find nearest unit in gameobject array
    {                                                                                               //
        nearestObj = null;                                                                          //
        shortDistance = Mathf.Infinity;                                                             // reset distance to infinity if nearest unit is null
        foreach (GameObject obj in array)                                                           // loop through all units in scene
        {                                                                                           //
            if (obj == null || obj == this.gameObject) continue;                                    // skip the iteration if the unit is null or itself
            if (!CalcPath(tempPath, obj)) continue;                                                 // if path is interrupted, skip iteration
            if (PathDistance(tempPath, Color.yellow,false) < shortDistance)                         // if we find a unit that is closer than the others, we obtain their references
            {                                                                                       //
                nearestObj = obj;                                                                   // identify nearest unit
                shortDistance = PathDistance(tempPath, Color.yellow, false);                        //
            }
        }
    }

    #endregion
}