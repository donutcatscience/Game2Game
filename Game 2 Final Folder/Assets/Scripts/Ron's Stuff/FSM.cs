using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class FSM : StateMachineBehaviour
{
    // NPC will grab access of the minion's arttributes
    public BaseVariables NPC = null;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //set the NPC variable here if it is not already set
        if(NPC == null) NPC = animator.gameObject.transform.root.gameObject.GetComponent<BaseVariables>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        #region kill this minion when health <= 0
        if(NPC.health <= 0f) NPC.isDead = true;
        if (NPC.isDead) { NPC.anim.SetBool("isDead", true); NPC.agent.speed = 0f; }
        #endregion

        #region remove enemy minions from nearby units when they die
        foreach (GameObject minion in NPC.nearbyUnits.ToList())
        {
            if (minion.GetComponent<BaseVariables>().isDead)
            {
                NPC.nearbyUnits.Remove(minion);
            }
        }
        #endregion

        #region set flags based on nearbyUnits count
        // if there are no nearby units, set flags to false
        if (NPC.nearbyUnits.Count == 0)
        {
            NPC.anim.SetBool("nearbyMinion", false);
            NPC.anim.SetBool("inMeleeRange", false);
            NPC.targetMinion = null;
        }
        // else there is a minion nearby
        else
        {
            #region order nearby units by distance
            if (NPC.nearbyUnits.Count >= 2)
            {
                NPC.nearbyUnits = NPC.nearbyUnits.OrderBy(x => Vector3.Distance(NPC.transform.position, x.transform.position)).ToList();
            }
            #endregion

            NPC.anim.SetBool("nearbyMinion", true);
            NPC.targetMinion = NPC.nearbyUnits[0];
        }
        #endregion







        // if there is a valid target
        if (NPC.targetMinion != null)
        {
            // if target is in range
            if (Vector3.Distance(NPC.transform.position, NPC.targetMinion.gameObject.transform.position) < NPC.attackRadius)
            {
                // set inMeleeRange to true
                NPC.anim.SetBool("inMeleeRange", true);
            }
            // otherwise set false
            else NPC.anim.SetBool("inMeleeRange", false);
        }
        else NPC.anim.SetBool("inMeleeRange", false);


        



    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }
}
