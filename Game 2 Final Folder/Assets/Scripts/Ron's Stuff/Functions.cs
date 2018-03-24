using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Functions : MonoBehaviour
{
    public BaseVariables NPC;

    void Awake()
    {
        // get reference to this minion's atrtributes
        NPC = GetComponent<BaseVariables>();
    }

    void Follow()
    {
        if (NPC.targetMinion != null) NPC.agent.SetDestination(NPC.targetMinion.transform.position);
        print("calculating nearest path to enemy");
    }

    /// <summary>
    /// Removes dead minions from the list of minion references that is passed in
    /// </summary>
    /// <param name="list"></param>
    public void RemoveFromListWhenDead(List<GameObject> list)
    {
        foreach (GameObject minion in list.ToList())
        {
            if (minion.GetComponent<BaseVariables>().isDead)
            {
                list.Remove(minion);
            }
        }
    }
}
