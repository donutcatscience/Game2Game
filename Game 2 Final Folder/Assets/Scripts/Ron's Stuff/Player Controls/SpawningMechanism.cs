using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class SpawningMechanism : MonoBehaviour
{
    private bool canSpawn;
    private float timer;
    private float count;
    public float countMult;
    public float spawnDelay;
    public BaseVariables minionPrefab;
    public MinionSide side;

    public int[] lengths = new int[3];

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

            for (int i = 0; i < lengths[0]; i++)
            {
                Spawn(side,MinionType.Melee);
                count += countMult;
            }

            for (int i = 0; i < lengths[1]; i++)
            {
                Spawn(side,MinionType.Healer);
                count += countMult;
            }

            for (int i = 0; i < lengths[2]; i++)
            {
                Spawn(side,MinionType.Ranged);
                count += countMult;
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




    public void Spawn(MinionSide side, MinionType type)
    {
        BaseVariables minion = Instantiate(minionPrefab);
        minion.minionType = type;
        minion.minionSide = side;
        minion.transform.position += new Vector3(count, 0, 0);
    }

}
*/