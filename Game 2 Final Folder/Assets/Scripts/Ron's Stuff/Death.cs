using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : FSM {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Debug.Log("DEAD");
        NPC.targetMinion = null;
        //NPC.agent.speed = 0;
        NPC.nearbyAllyUnits.Clear();
        NPC.nearbyEnemyUnits.Clear();
        NPC.targetedByUnits.Clear();
        NPC.GetComponentInChildren<MinionNearbyTrigger>().enabled = false;
	}

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}
