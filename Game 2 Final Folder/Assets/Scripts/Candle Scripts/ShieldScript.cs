using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {

    public GameObject prefabToSpawn;
    private GameObject player;
    private Object spawnedColliderID;
    private Vector3 offset = new Vector3(0.0f, 3.0f, 0.0f);
    bool spawnedCollider = false;

    private void Start()
    {
        player = GameObject.Find("ThirdPersonController");
    }

    // Update is called once per frame
    void Update ()
    {
        transform.position = player.transform.position;
        transform.forward = player.transform.forward;

        if (Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift) && !spawnedCollider)
        {
            spawnedCollider = true;
            spawnedColliderID = Instantiate(prefabToSpawn, (player.transform.position + player.transform.forward + offset), player.transform.rotation);
        }

        if(!Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
        {
            spawnedCollider = false;
            Destroy(spawnedColliderID);
        }


	}
}
