using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStuff : MonoBehaviour
{
    private void Awake()
    {
        SpawnMinions(MinionSide.White, MinionType.Melee, transform);
    }


    public BaseVariables[] minionPrefab;

    public void SpawnMinions(MinionSide side, MinionType type, Transform location)
    {
        BaseVariables minion;

        switch (side)
        {
            case MinionSide.White:
                minion = Instantiate(minionPrefab[0]);
                break;

            default:
                minion = Instantiate(minionPrefab[1]);
                break;
        }
        minion.gameObject.transform.position = location.position;
    }
}
