using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillowCamera : MonoBehaviour
{

    Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion cameraRot;

    float elevation = 0.01f;
    float rotSpeed = 1000.0f;
    float speed = 1000.0f;
    bool moving = false;

    // Update is called once per frame
    void Update()
    {
        SetTargetPosition();
    }

    void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            targetPosition = hit.point;
            targetPosition.y = elevation;
            lookAtTarget = new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, targetPosition.z - transform.position.z);
            Quaternion.LookRotation(lookAtTarget);
            transform.LookAt(lookAtTarget);
            
        }
    }
}
