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
        if (NPC.targetEnemy != null) NPC.agent.SetDestination(NPC.targetEnemy.transform.position);
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

    public void OrderByDistance(List<GameObject> list)
    {
        // we only need to sort the order if there are more than 2 units in the list
        if (list.Count >= 2)
        {
            // sorting algorithm
            print("sorting list");
            list.Sort(delegate (GameObject c1, GameObject c2)
            {
                return Vector3.Distance(transform.position, c1.transform.position).CompareTo((Vector3.Distance(transform.position, c2.transform.position)));
            });
            
        }
    }

    public void OrderEnemies()
    {
        OrderByDistance(NPC.nearbyEnemyUnits);
    }
    public void OrderAllies()
    {
        OrderByDistance(NPC.nearbyAllyUnits);
    }

    public void Attack()
    {
        NPC.targetEnemy.GetComponent<BaseVariables>().health -= NPC.damage;
        Debug.Log("Attack");
    }



    public void HealerDestination()
    {
        // this vector will be the destination of this healer minion
        Vector3 center = new Vector3(0, 0, 0);
        // find out how many minions are in each list
        int countEnemy = NPC.nearbyEnemyUnits.Count();
        int countAlly = NPC.nearbyAllyUnits.Count();
        // if there are no units nearby, escape this function
        if (countAlly == 0 && countEnemy == 0)
        {
            print("NO ENEMIES OR ALLIES");
            NPC.agent.SetDestination(NPC.waypoint[NPC.minionSide == MinionSide.White ? 0 : 1].transform.position);
            return;
        }
        // find average vector3 between all nearby allies
        foreach (GameObject minion in NPC.nearbyAllyUnits)
        {
            // accumulate all locations of allies
            center += minion.transform.position;
        }
        // consider each enemy in range
        foreach (GameObject minion in NPC.nearbyEnemyUnits)
        {
            // calculate a position behind the player that lines up with the direction from this minion to the enemy unit
            Vector3 temp = NPC.transform.position + ((minion.transform.position - NPC.transform.position).normalized * -3);
            // add this vector to the center vector in order to offset the destination from enemies
            center += new Vector3(temp.x, minion.transform.position.y, temp.z);
        }
        // find the average of all positions accumulated
        center = center / (countEnemy + countAlly);
        NPC.agent.SetDestination(center);
    }
}
