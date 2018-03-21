using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : FSM
{
    public float timer = 0f;
    public float timerRange = 1f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.agent.speed = 0f;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timer == timerRange && NPC.targetMinion != null)
        {
            NPC.targetMinion.GetComponent<BaseVariables>().health -= NPC.damage;
            Debug.Log("Attack");
            timer = 0f;
        }

        timer += Time.deltaTime;
        timer = Mathf.Min(timer, timerRange);

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}
