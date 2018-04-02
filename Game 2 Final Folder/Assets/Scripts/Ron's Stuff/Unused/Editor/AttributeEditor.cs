/*
using UnityEngine;
using System.Collections;
using UnityEditor;

// ------------------------------------------------------------------------------------
// CLASS	:	AIWaypointNetworkEditor
// DESC		:	Custom Inspector and Scene View Rendering for the AIWaypointNetwork
//				Component
// ------------------------------------------------------------------------------------
[CustomEditor(typeof(MinionAttributes))]
public class AIWaypointNetworkEditor : Editor
{
    // --------------------------------------------------------------------------------
    // Name	:	OnInspectorGUI (Override)
    // Desc	:	Called by Unity Editor when the Inspector needs repainting for an
    //			AIWaypointNetwork Component
    // --------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        // Get reference to selected component
        MinionAttributes attr = (MinionAttributes)target;

        // Show the Display Mode Enumeration Selector
        attr.s1 = EditorGUILayout.FloatField("Speed", 0f);


        // Tell Unity to do its default drawing of all serialized members that are NOT hidden in the inspector
        DrawDefaultInspector();

        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }
}
*/
