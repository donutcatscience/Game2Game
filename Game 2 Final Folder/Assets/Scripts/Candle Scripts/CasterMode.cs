using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterMode : MonoBehaviour {
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force;
    public int fireRate = 15;
    public int leftCastCost = 5;

    private int frameCount = 0;
    private bool fire = false;
    private int currentGemCount;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) {
            GetComponent<RunSpeedIncrease>().bonusRunSpeed = 0;
        }
        else
        {
            GetComponent<RunSpeedIncrease>().bonusRunSpeed = 1;
        }

        currentGemCount = GetComponent<PlayerMovement>().gemCount;
        if (Input.GetKey(KeyCode.Space) && Input.GetMouseButton(0) && frameCount > fireRate && currentGemCount >= leftCastCost)
        {
            fire = true;
            GetComponent<PlayerMovement>().gemCount -= leftCastCost;
        }

        if (fire)
        {
            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force. 
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

            //set the Bullets to self destruct after 3
            Destroy(Temporary_Bullet_Handler, 3.0f);
            fire = false;
            frameCount = 0;
        }
        ++frameCount;
    }
}
