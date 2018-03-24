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
        NPC.coroutines.InvokeRepeating("Follow", 0f, 0.6f);


        if (NPC.targetMinion != null)
        {
            if (!NPC.targetMinion.GetComponent<BaseVariables>().targetedByUnits.Contains(NPC.gameObject))
            {
                NPC.targetMinion.GetComponent<BaseVariables>().targetedByUnits.Add(NPC.gameObject);
            }
        }

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
        NPC.coroutines.CancelInvoke("Follow");
    }



}
