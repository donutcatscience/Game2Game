using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : MonoBehaviour {

    private GameObject player;
    private Vector3 offset = new Vector3(0.0f, 3.0f, 0.0f);
    DamageSparkSpawn vfxHandler; //add this line for spark spawning

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("ThirdPersonController");
        vfxHandler = gameObject.GetComponent<DamageSparkSpawn>(); //add this line for spark spawning
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.forward = player.transform.forward;
        transform.position = player.transform.position + player.transform.forward + offset;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy Projectile")) 
        {
            {
                vfxHandler.isHit = true; //add this line for spark spawning
                Destroy(other.gameObject);
            }
        }
    }
}
