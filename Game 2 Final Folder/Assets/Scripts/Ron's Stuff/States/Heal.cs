using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : FSM {

    public float timer = 0f;
    public float timerRange = 0.2f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.agent.speed = 0f;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timer == timerRange && NPC.nearbyAllyUnits.Count != 0)
        {
            NPC.nearbyAllyUnits[0].GetComponent<BaseVariables>().health += NPC.damage*1.25f;
            Debug.Log("Heal");
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
