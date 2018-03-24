using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionNearbyTrigger : Triggers
{
    public override void Start()
    {
        base.Start();
    }

    void OnTriggerEnter(Collider other)
    {
        // if the minion that entered our range is not dead
        if (!other.GetComponent<BaseVariables>().isDead)
        {
            // check if the minion is not already in our list of nearby minions
            if (!NPC.nearbyEnemyUnits.Contains(other.gameObject))
            {
                // is the minion not on the same side?
                if (other.GetComponent<BaseVariables>().minionSide != NPC.minionSide)
                {
                    // add the minion to the enemy list
                    NPC.nearbyEnemyUnits.Add(other.gameObject);
                }
                // otherwise the minion is on our side
                else
                {
                    // add the minion to the ally list
                    NPC.nearbyAllyUnits.Add(other.gameObject);
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        functions.RemoveFromListWhenDead(NPC.nearbyEnemyUnits);
        functions.RemoveFromListWhenDead(NPC.nearbyAllyUnits); 
    }

    void OnTriggerExit(Collider other)
    {
        // if the minion that exit our range is not dead
        if (!other.GetComponent<BaseVariables>().isDead)
        {
            // is the minion on our side?
            if(other.GetComponent<BaseVariables>().minionSide == NPC.minionSide)
            {
                // remove from ally list
                NPC.nearbyAllyUnits.Remove(other.gameObject);
            }
            // otherwise the minion is not on our side
            else
            {
                // remove from enemy list
                NPC.nearbyEnemyUnits.Remove(other.gameObject);
            }
        }
    }




}
