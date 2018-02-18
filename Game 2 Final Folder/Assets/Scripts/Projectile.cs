using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Projectile : MonoBehaviour
    {
        GameObject[] enemies;//temporary

        private Transform target;
        public Rigidbody bullet;

        private float acceleration = 100;//bullet speed;
        private float distance;
        //PlayerController pc = GetComponent<PlayerController>();
        
         private bool inRange = false;

    // Use this for initialization
    void Start()
    {
            InvokeRepeating("fire", 1.0f, .3f);//starting at 2 seconds, fire will be called every 1 second
    }

        // Update is called once per frame
        void Update()
        {
        //temporary code until below code is fixed. 
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        target = enemies[0].transform;
        distance = Vector3.Distance(transform.position, target.position);

        //attempted to call already defined methods in player controller. couldnt solve my issues. 
        //distance = PlayerController.
        //target = pc.getNearestEnemy().transform;

        if (distance < 25 && distance > 1)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        //face target
        if (inRange)
        {
            transform.LookAt(target.transform.position);
        }
        }
        void fire()
        {
        if (inRange && distance > 1)
        {
            print("Fire!\n");//test statement
            var newBullet = (Rigidbody)Instantiate(bullet, transform.position + transform.forward, transform.rotation);//creates bullet at character location. 
            newBullet.AddForce(transform.forward * acceleration, ForceMode.Impulse);//adds velocity to bullet causing motion
            Destroy(newBullet, 2.0f);// removes bullet object after 2 seconds

        }
        }
    }
