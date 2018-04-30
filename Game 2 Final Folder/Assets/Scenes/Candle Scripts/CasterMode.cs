using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterMode : MonoBehaviour {
    public int fireRate = 15;
    public GameObject Bullet_Emitter;

    //left cast publics
    public int leftCastCost = 5;
    public GameObject Bullet;
    public AudioClip castSound;

    //right cast publics
    public int rightCastCost = 10;
    public GameObject rightSpellBullet;
    public AudioClip rightCastSound;

    public float range = 1000f;
    public float Bullet_Forward_Force = 500;
    public ParticleSystem mainSpellFlash;
    

    public Vector3 targetPosition;
    public Vector3 lookAtTarget;

    private int frameCount = 0;
    private bool fireLeft = false;
    private bool fireRight = false;
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

        //Left Spell Cast (will come clean this up later)
        if (Input.GetKey(KeyCode.Space) && Input.GetMouseButton(0) && frameCount > fireRate && currentGemCount >= leftCastCost)
        {
            fireLeft = true;
            GetComponent<PlayerMovement>().gemCount -= leftCastCost;
        }

        if (fireLeft)
        {
            //Still need initial cast particl effect
            //mainSpellFlash.GetComponent<ParticleSystem>().Emit(100);

            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                targetPosition = hit.point;
                lookAtTarget = new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, targetPosition.z - transform.position.z);
                lookAtTarget = lookAtTarget * Bullet_Forward_Force;
                Temporary_RigidBody.AddForceAtPosition(transform.forward + lookAtTarget , hit.point);

                //set the Bullets to self destruct after 3
                Destroy(Temporary_Bullet_Handler, 3.0f);
                fireLeft = false;
                frameCount = 0;
            }
        }

        //Right Spell Cast (will come clean this up later)
        if (Input.GetKey(KeyCode.Space) && Input.GetMouseButton(1) && frameCount > fireRate && currentGemCount >= rightCastCost)
        {
            fireRight = true;
            GetComponent<PlayerMovement>().gemCount -= rightCastCost;
        }

        if (fireRight)
        {
            //Still need initial cast particl effect
            //mainSpellFlash.GetComponent<ParticleSystem>().Emit(100);

            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(rightSpellBullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                targetPosition = hit.point;
                lookAtTarget = new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, targetPosition.z - transform.position.z);
                lookAtTarget = lookAtTarget * Bullet_Forward_Force;
                Temporary_RigidBody.AddForceAtPosition(transform.forward + lookAtTarget, hit.point);

                //set the Bullets to self destruct after 3
                Destroy(Temporary_Bullet_Handler, 3.0f);
                fireRight = false;
                frameCount = 0;
            }
        }
        //control fire rate
        ++frameCount;
    }
}

