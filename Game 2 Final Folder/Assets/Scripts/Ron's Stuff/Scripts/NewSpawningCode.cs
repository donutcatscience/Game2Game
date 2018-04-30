using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawningCode : MonoBehaviour {

    public GameObject minionPrefab;
    public static int minionAmount = 0;
    float time = 0f;
    public float spawnDelay = 1f;
    public Vector2 sizeRange;
    public MinionSide minionSide;



    private void Update()
    {

        time += Time.deltaTime;

        int size = Random.Range((int)sizeRange.x, (int)sizeRange.y);
        if(time >= spawnDelay && minionAmount <= 70)
        {
            for (int i = 0; i < size; i++)
            {
                SpawnMinion(minionSide, MinionType.Melee);
            }
            time = 0f;
        }
        print("SIZE: " + minionAmount);
    }

    public void SpawnMinion(MinionSide side, MinionType type)
    {
        BaseVariables minion = Instantiate(minionPrefab, transform.position, Quaternion.identity).GetComponent<BaseVariables>();
        //BaseVariables min = minion.GetComponent<BaseVariables>();
        minion.minionType = type;
        minion.minionSide = side;
        minionAmount++;
    }

}
