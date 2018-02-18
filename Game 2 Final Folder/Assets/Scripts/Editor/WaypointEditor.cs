using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[CustomEditor(typeof(WaypointNetwork))]
public class WaypointEditor : Editor
{
    

    public override void OnInspectorGUI()
    {
        WaypointNetwork network = (WaypointNetwork)target;
        network.start = EditorGUILayout.IntSlider("start", network.start, 0, network.Waypoints.Count - 1);
        network.end = EditorGUILayout.IntSlider("end", network.end, 0, network.Waypoints.Count - 1);
        DrawDefaultInspector();
    }
    void OnSceneGUI()
    {
        WaypointNetwork network = (WaypointNetwork)target;

        Vector3[] linePoints = new Vector3[network.Waypoints.Count];
        for (int i = 0; i < network.Waypoints.Count; i++)
        { 
            linePoints[i] = network.Waypoints[i].position;
            Handles.Label(network.Waypoints[i].position, "Waypoint " + (i + 1).ToString());
        }
        if (network.displayMode == PathDisplayMode.Connections)
        {
            Handles.color = Color.red;
            Handles.DrawPolyLine(linePoints);
        }
        else
        {
            NavMeshPath path = new NavMeshPath();
            Vector3 from = network.Waypoints[network.start].position;
            Vector3 to = network.Waypoints[network.end].position;
            NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);
            Handles.DrawPolyLine(path.corners);
        }
        
    }

}
