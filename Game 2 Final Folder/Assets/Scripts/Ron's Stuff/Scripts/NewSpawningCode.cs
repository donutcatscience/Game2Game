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


    private void Start()
    {
        minionAmount = 0;
    }
    private void Update()
    {
        print("UPDATE");
        time += Time.deltaTime;

        
        if(time >= spawnDelay && minionAmount <= 70)
        {
            int size = Random.Range((int)sizeRange.x, (int)sizeRange.y);
            for (int i = 0; i < size; i++)
            {
                int rand = Random.Range(1, 10);
                MinionType type;
                if (rand == 1) type = MinionType.Healer;
                else if(rand >= 2 && rand <= 7) type = MinionType.Melee;
                else type = MinionType.Ranged;

                SpawnMinion(minionSide, type);
            }
            time = 0f;
        }
    }

    public void SpawnMinion(MinionSide side, MinionType type)
    {
        BaseVariables minion = Instantiate(minionPrefab, transform.position, Quaternion.identity).GetComponent<BaseVariables>();
        //BaseVariables min = minion.GetComponent<BaseVariables>();
        minion.minionType = type;
        minion.minionSide = side;
    }

}
