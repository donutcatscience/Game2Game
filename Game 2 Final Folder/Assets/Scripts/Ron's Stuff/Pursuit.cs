using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pursuit : FSM
{
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.agent.speed = NPC.speed;
        //NPC.coroutines.StartCoroutine(Coroutines.Follow(NPC));
        if(NPC.targetMinion != null) NPC.agent.SetDestination(NPC.targetMinion.transform.position);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (NPC.targetMinion != null) NPC.agent.SetDestination(NPC.targetMinion.transform.position);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
        //NPC.coroutines.StopCoroutine(Coroutines.Follow(NPC));
    }
}
