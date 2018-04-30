using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningMechanism : MonoBehaviour
{
    public GameObject[] minionType1;
    public GameObject[] minionType2;
    public GameObject[] minionType3;

    private bool canSpawn;
    private float timer;
    private float count;
    public float spawnDelay;
   

	// Use this for initialization
	void Start ()
    {
        canSpawn = true;
        timer = 0f;
        count = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(canSpawn)
        {
            canSpawn = false;

            for (int i = 0; i < minionType1.Length; i++)
            {
                Spawn(minionType1[i]);
                count += 1f;
            }

            for (int i = 0; i < minionType2.Length; i++)
            {
                Spawn(minionType2[i]);
                count += 1f;
            }

            for (int i = 0; i < minionType3.Length; i++)
            {
                Spawn(minionType3[i]);
                count += 1f;
            }
        }

        timer += Time.deltaTime;
        if(timer >= spawnDelay)
        {
            canSpawn = true;
            timer = 0f;
            count = 0f;
        }
	}

   void Spawn(GameObject myGameObject)
   {
        GameObject minion = Instantiate(myGameObject) as GameObject;

        minion.transform.parent = transform;
        minion.transform.position = transform.position + new Vector3(count, 0, 0);
   }
}
