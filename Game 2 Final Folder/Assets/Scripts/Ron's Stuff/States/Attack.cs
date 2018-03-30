using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : FSM
{
    #region OnStateEnter
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // set speed to 0
        NPC.agent.speed = 0f;
        // invoke attack method
        if(!NPC.functions.IsInvoking("Attack")) NPC.functions.InvokeRepeating("Attack", 0f, 1f);
    }
    #endregion
    #region OnStateUpdate
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
    #endregion
    #region OnStateExit
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
        // cancel attack invoke
        if(NPC.functions.IsInvoking("Attack")) NPC.functions.CancelInvoke("Attack");
    }
    #endregion
}
