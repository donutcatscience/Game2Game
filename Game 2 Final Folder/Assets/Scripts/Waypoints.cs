using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] points; // array of waypoints set in inspector

    void Awake()
    {
        points = new Transform[transform.childCount]; // find the size of the array
        for (int i = 0; i < points.Length; i++) // instantiate each instance in the array
        {
            points[i] = transform.GetChild(i); // obtain the transforms of the waypoint
        }
    }
}
