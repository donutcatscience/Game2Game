using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static GameObject[] points; // array of waypoints set in inspector

    public int waypointIndex = 0;

    void Awake()
    {
        points = new GameObject[transform.childCount]; // find the size of the array
        for (int i = 0; i < points.Length; i++) // instantiate each instance in the array
        {
            points[i] = transform.GetChild(i).gameObject; // obtain the transforms of the waypoint
        }
    }
}
