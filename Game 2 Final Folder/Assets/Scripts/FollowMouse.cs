using UnityEngine;

public class FollowMouse : MonoBehaviour {

    Vector3 targetPosition;
    Vector3 lookAtTarget;

    public float elevation = 0.01f;
    public bool elevatedTerrain;

    float rotSpeed = 1000.0f;
    float speed = 1000.0f;
    bool moving = false;

	// Update is called once per frame
	void Update () {
            SetTargetPosition();
            Move();
    }

    void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            targetPosition = hit.point;         
            if (elevatedTerrain)
            {
                float ypos = Terrain.activeTerrain.SampleHeight(new Vector3(hit.point.x, 0, hit.point.z)) + 0.85f;
                targetPosition.y = ypos;
            } else
            {
                targetPosition.y = elevation;
            }

            lookAtTarget = new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, targetPosition.z - transform.position.z);
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
