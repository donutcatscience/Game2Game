using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathDisplayMode {Connections, Paths}

[System.Serializable]
public struct Link
{
    public enum direction {UNI,BI};
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WaypointNetwork : MonoBehaviour
{
    [HideInInspector]
    public int start;
    [HideInInspector]
    public int end;

    public PathDisplayMode displayMode = PathDisplayMode.Connections;
    public List<Transform> Waypoints = new List<Transform>();
    public List<Link> links = new List<Link>();



}
