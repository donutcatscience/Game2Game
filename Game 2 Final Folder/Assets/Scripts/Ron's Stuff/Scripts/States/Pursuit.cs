using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pursuit : FSM
{
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // set pursuit speed
        NPC.agent.speed = NPC.speed;
        // set destination of target enemy every x seconds
        NPC.InvokeRepeating("Follow", 0f, 0.5f);

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
        // cancel the invoke of destination calls
        NPC.CancelInvoke("Follow");
    }



}
