using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : FSM
{
    #region OnstateEnter
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // set patrol speed
        NPC.agent.speed = NPC.speed*0.5f;
        // set destination
        if (NPC.minionType != MinionType.Healer) NPC.agent.SetDestination(NPC.waypoint[NPC.minionSide == MinionSide.White ? 1 : 0].transform.position);
        else if(!NPC.IsInvoking("HealerDestination"))NPC.InvokeRepeating("HealerDestination",0f,0.3f);
        
    }
    #endregion
    #region OnStateUpdate
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        #region recalculate path if stale
        if (NPC.agent.isPathStale || !NPC.agent.hasPath || NPC.agent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            Debug.Log(NPC.name + ": Path is stale; recalculating...");
            if (NPC.minionType != MinionType.Healer) NPC.agent.SetDestination(NPC.waypoint[NPC.minionSide == MinionSide.White ? 1 : 0].transform.position);
        }
        #endregion
    }
    #endregion
    #region OnStateExit
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
        if(NPC.IsInvoking("HealerDestination"))NPC.CancelInvoke("HealerDestination");
    }
    #endregion
}
