using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class PlayerController : MonoBehaviour {

    #region variables
    NavMeshAgent agent;                     // get reference to the agent; used for navigation
    NavMeshPath primaryPath;                // stores the path to wawypoints
    NavMeshPath secondaryPath;              // stores the path to nearest unit
    NavMeshPath tempPath;                   // stores any other paths the agent might consider

    GameObject[] units;                     // array of all units in scene
    GameObject nearestUnit;                 // stores the nearest enemy

    public  float aggroRange;               // the distance in which the unit will target nearby enemy units
    public  float primaryPathDist;          // stores the distance of the primary path
    public  float secondaryPathDist;        // stores the distance of the secondary path
            float shortestDistance;         // shortest distance between this enemy & other enemies

    int waypointIndex;                      // traverses array of units in scene

    public GameObject primaryTarget;        // stores the target of the primary path
    public GameObject secondaryTarget;      // stores the target of the secondary path
    public GameObject tempTarget;           // used for swapping the targets

    Targeting targeting;                    // used for switching of states
    #endregion

    public enum Targeting                   // Mini-state machine that determines whether the unit is targeting waypoints or units
    {
        units,
        waypoints
    };

    void Start ()
    {
        nearestUnit = null;                                                 // nearest enemy will be computed in Update
        agent = GetComponent<NavMeshAgent>();                               // obtain agent
        primaryPath = new NavMeshPath();                                    // create path
        secondaryPath = new NavMeshPath();                                  // create path
        tempPath = new NavMeshPath();                                       // temporary path if necessary
        waypointIndex = 0;                                                  // set position in array of waypoints
        primaryTarget = Waypoints.points[waypointIndex].gameObject;         // set primary target to first waypoint in array
        secondaryTarget = nearestUnit;                                      // set secondary target to nearest unit
        units = ListOfUnits.units;                                          // identify enemies in scene; MUST PUT THIS IN UPDATE WHEN ENEMIES SPAWN DURING GAMEPLAY
        
        shortestDistance = Mathf.Infinity;                                  // set distance between this unit & non-existing nearby unit to infinity
        targeting = Targeting.waypoints;                                    // set state to target waypoints by default
        aggroRange = 8f;                                                    // set aggro range to 8 units
    }

    void Update()
    {
        if (CalcPath(primaryPath, primaryTarget)) agent.SetPath(primaryPath);                                                                               // if the path is uninterrupted, set the path; otherwise, clear the path
        else agent.ResetPath();

        if (Vector3.Distance(transform.position, Waypoints.points[waypointIndex].position) < .3f && targeting == Targeting.waypoints) GetNextWaypoint();    // get the next waypoint if current waypoint is reached within distance

        #region find nearest unit       // WILL BE REWORKING THIS SOON, it works... for now.
        if (nearestUnit == null) shortestDistance = Mathf.Infinity;                                                                                         // reset distance to infinity if nearest unit is null
        foreach (GameObject unit in units)                                                                                                                  // loop through all units in scene
        {
            if (unit == null || unit == this.gameObject) continue;                                                                                          // skip the iteration if the unit is null or itself
            if (Vector3.Distance(transform.position, unit.transform.position) < shortestDistance)                                                           // if we find a unit that is closer than the others, we obtain their references
            {
                nearestUnit = unit;                                                                                                                         // identify nearest unit
                shortestDistance = Vector3.Distance(transform.position, nearestUnit.transform.position);                                                    // set the distance between self & nearest unit       
            }
        }
        #endregion
                                                                                                                                                            // REWORK NEEDED
        if (targeting == Targeting.waypoints)                                                                                                               // while the unit is focused on waypoints,                                                     
        {
            secondaryTarget = nearestUnit;                                                                                                                  // set the secondary target to the nearest unit
        }

        CalcPath(secondaryPath, secondaryTarget);                                                                                                           // calculate path to nearest unit

        primaryPathDist = PathDistance(primaryPath, Color.red);                                                                                             // calculate path distance for primary path & show path in inspector
        secondaryPathDist = PathDistance(secondaryPath, Color.blue);                                                                                        // calculate path distance for secondary path & show path in inspector

        if (secondaryPathDist < aggroRange && targeting != Targeting.units)                                                                                 // if the unit is not already targeting other units & another unit is within range,
        {
            targeting = Targeting.units;                                                                                                                    // target the unit
            SwapTargets();                                                                                                                                  // swap targets so primary path is now targeting unit
        }

        if(targeting == Targeting.units)                                                                                                                    // if unit is targeting other unit
        {
            if (nearestUnit == null)                                                                                                                        // if the nearest enemy no longer exists; i.e.  nearest enemy is dead
            {
                targeting = Targeting.waypoints;                                                                                                            // go back to targeting waypoints
                SwapTargets();                                                                                                                              // swap the targets back
            }
        }

    }

    #region functions
    float PathDistance(NavMeshPath path, Color color)                               // returns the distance of the path & draws path in editor
    {
        float pathDist = 0f;                                                        // reset distance
        for (int i = 0; i < path.corners.Length - 1; i++)                           // calculate for every corner of the path
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], color);            // draw line from current corner to next corner
            pathDist += Vector3.Distance(path.corners[i], path.corners[i + 1]);     // accumulate distance from current corner to next
        }
        return pathDist;                                                            // return total distance of path
    }

    bool CalcPath(NavMeshPath path, GameObject target)                              // Calculates a path from self to a target destination; returns true if path is uninterrupted
    {
        if (target == null)                                                         // if our target does not exist, reset our path
        {
            agent.CalculatePath(transform.position, path);
            return false;
        }
        agent.CalculatePath(target.transform.position, path);                       // calculates the path
        if (path.status == NavMeshPathStatus.PathPartial) return false;             // return false if interrupted
        else return true;                                                           // return true if intact
    }

    void GetNextWaypoint()                                                          // Cycles through array of waypoints
    {
        waypointIndex++;                                                            // iterate to next waypoint in array
        if (waypointIndex >= Waypoints.points.Length)                               // check if index reaches out of bounds
        {
            waypointIndex = 0;                                                      // if so, set it back to the beginning
        }
        primaryTarget = Waypoints.points[waypointIndex].gameObject;                 // set the waypoint
    }

    void SwapTargets()                                                              // swaps the targets between waypoint & nearest unit
    {
        tempTarget = primaryTarget;
        primaryTarget = secondaryTarget;
        secondaryTarget = tempTarget;
    }
    #endregion
}
