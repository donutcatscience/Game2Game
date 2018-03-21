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
        if (other.GetComponent<BaseVariables>().isDead == false)
        {
            if (other.GetComponent<BaseVariables>().minionSide != NPC.minionSide)
            {
                if(!NPC.nearbyUnits.Contains(other.gameObject))
                {
                    NPC.nearbyUnits.Add(other.gameObject);
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BaseVariables>().isDead == false)
        {
            if (other.GetComponent<BaseVariables>().minionSide != NPC.minionSide)
            {
                NPC.nearbyUnits.Remove(other.gameObject);
            }
        }
    }
}
