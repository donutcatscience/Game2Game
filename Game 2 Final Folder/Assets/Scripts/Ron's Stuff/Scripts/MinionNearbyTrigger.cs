using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionNearbyTrigger : Triggers
{

    BaseVariables minion;


    public override void Start()
    {
        base.Start();
        
    }

    void OnTriggerEnter(Collider other)
    {
        print("collision");
        minion = other.GetComponent<BaseVariables>();
        // if the minion that entered our range is not dead
        if (!minion.isDead)
        {
            // is the minion not on the same side?
            if (minion.minionSide != NPC.minionSide)
            {
                // check if the minion is not already in our list of nearby enemies
                if (!NPC.nearbyEnemyUnits.Contains(minion))
                {
                    // add the minion to the enemy list
                    NPC.nearbyEnemyUnits.Add(minion);
                }
            }
            // otherwise the minion is on our side
            else
            {
                // check if the minion is not already in our list of nearby allies
                if (!NPC.nearbyAllyUnits.Contains(minion))
                {
                    // add the minion to the ally list
                    NPC.nearbyAllyUnits.Add(minion);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        minion = other.GetComponent<BaseVariables>();
        // if the minion that exit our range is not dead
        if (!minion.isDead)
        {
            // is the minion on our side?
            if(minion.minionSide == NPC.minionSide)
            {
                // remove from ally list
                NPC.nearbyAllyUnits.Remove(minion);
            }
            // otherwise the minion is not on our side
            else
            {
                // remove from enemy list
                NPC.nearbyEnemyUnits.Remove(minion);
            }
        }
    }




}
