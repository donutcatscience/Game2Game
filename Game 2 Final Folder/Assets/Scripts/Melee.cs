using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {


    public GameObject[] enemies;//temporary code
    public Transform target;
    public float distance;
    public bool inRange = false;

    void Start () {
        InvokeRepeating("attack", 2.0f, 1.0f);
    }
	
	void Update () {
        //temporary code until bug is fixed
        enemies = GameObject.FindGameObjectsWithTag("Enemy");//fills array with every available object containing enemy tag
        target = enemies[1].transform;//temporary direct assignment to test object
        distance = Vector3.Distance(transform.position, target.position);

        if (distance < 1)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        
    }

    void attack()
    {
        if (inRange)//if target is within melee range
        {
            print("Swing!" + "\n");//Swing!
            print(target.gameObject.name + " takes one point of damage");//"Current Target" takes one point of damage!
            target.GetComponent<NPCHealth>().testDamage();//invokes test function for dealing damage
        }
    }
}
