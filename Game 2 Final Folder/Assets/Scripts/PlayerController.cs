using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class PlayerController : MonoBehaviour {

    Camera cam;  // get reference to the main camera
    [SerializeField] LayerMask movementMask; // reference to Ground layer; set in the inspector
    NavMeshAgent agent; // get reference to the agent; used for navigation
    int raycastRange = 1000; // range of raycast for click


    void Start () {
        cam = Camera.main; // obtain camera reference
        agent = GetComponent<NavMeshAgent>(); // obtain agent
    }
	
	void Update () {
		if(Input.GetMouseButtonDown(0)) // On left mouse button press
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // cast a ray where the user clicked
            RaycastHit hit; // obtain information on what was clicked

            if (Physics.Raycast(ray, out hit, raycastRange, movementMask)) // if the mouse clicked on the Ground layer, move the agent to the point
            {
                MoveToPoint(hit.point); // calls a function to move agent
            }
        }
	}

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
}
