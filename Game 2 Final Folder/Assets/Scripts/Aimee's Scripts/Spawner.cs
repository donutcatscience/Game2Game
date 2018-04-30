using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script will be attached to the spawner placeholders/empty objects
public class Spawner : MonoBehaviour
{
    // Creating an array to hold different type of enemies 
    // Variable is public so we can change the size and adjust how many different types of enemies are spawned at a spawning location.
    public GameObject[] attackerPrefabArray;

	
	// Update is called once per frame
	void Update ()
    {
        // Udemy's version of the code
        /*
        foreach (GameObject thisAttacker in attackerPrefabArray)
        {
            if (isTimeToSpawn(thisAttacker))
            {
                Spawn(thisAttacker);
            }
        }
        */

        // My version of the code, should still implement well. 
        // Going to double check with Selgrad regardless
        for (int i = 0; i < attackerPrefabArray.Length; i++)
        {
            if (isTimeToSpawn(attackerPrefabArray[i]))
            {
                Spawn(attackerPrefabArray[i]);
            }
        }


    }

    void Spawn (GameObject myGameObject)
    {
        // Spawning/instantiating the element minion within the array
        GameObject myAttacker = Instantiate(myGameObject) as GameObject;

        // Making sure it spawns where the empty obj Spawner is placed
        myAttacker.transform.parent = transform;
        myAttacker.transform.position = transform.position;
    }


    // Controls how far apart the enemies are spawner (time delay)
    bool isTimeToSpawn (GameObject attackerGameObject)
    {
        // Creating an object (attacker) from class AttackerMove
        AttackerMove attacker = attackerGameObject.GetComponent<AttackerMove>();

        // Retrieving the variable "seenEverSeconds" (which we can designate how often a minion appears)
        float meanSpawnDelay = 0f;
        float spawnsPerSecond = 1 / meanSpawnDelay;

        if(Time.deltaTime > meanSpawnDelay)
        {
            Debug.LogWarning("spawn rate capped by frame rate");
        }

        // Dividing by 5 because there are 5 empty spawning rows
        float threshold = (spawnsPerSecond * Time.deltaTime) / 5;

        if (Random.value < threshold)
        {
            return true;
        }

        else
        {
            return false;
        }

       return true;
    }


}
