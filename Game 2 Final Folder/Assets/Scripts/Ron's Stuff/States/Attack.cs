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
    }
    #endregion
    #region OnStateUpdate
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (NPC.targetEnemy != null)
        {
            Vector3 look = NPC.targetEnemy.transform.position - NPC.transform.position;
            look.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(look);
            NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, rotation, 2f * Time.deltaTime);
        }
    }
    #endregion
    #region OnStateExit
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
    #endregion
}
