using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterMode : MonoBehaviour {
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) {
            //prevent player from moving in caster mode
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
            //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force. 
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

            //set the Bullets to self destruct after 3
            Destroy(Temporary_Bullet_Handler, 3.0f);
        }
    }
}
