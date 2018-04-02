using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : FSM {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.agent.speed = NPC.speed;
        NPC.agent.SetDestination(NPC.waypoint[NPC.minionSide == MinionSide.White? 0:1].transform.position);

        NPC.targetEnemy = null;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
    }


}
