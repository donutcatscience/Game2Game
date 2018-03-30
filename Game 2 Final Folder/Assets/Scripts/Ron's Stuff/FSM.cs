using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class FSM : StateMachineBehaviour
{
    #region Variable declaration
    // NPC will grab access of the minion's arttributes; it can only be assigned in OnStateEnter
    public BaseVariables NPC = null;
    #endregion
    #region Hashes
    public int _isDead = Animator.StringToHash("isDead");
    public int _nearbyEnemy = Animator.StringToHash("nearbyEnemy");
    public int _nearbyAlly = Animator.StringToHash("nearbyAlly");
    public int _inMeleeRange = Animator.StringToHash("inMeleeRange");
    public int _criticalHealth = Animator.StringToHash("criticalHealth");
    #endregion

    #region OnStateEnter
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //set the NPC variable here if it is not already set
        if (NPC == null) NPC = animator.gameObject.transform.root.gameObject.GetComponent<BaseVariables>();
    }
    #endregion
    #region OnStateUpdate
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        #region set flags based on nearbyUnits count
            #region enemies
            // if there are no nearby units, set flags to false
            if (NPC.nearbyEnemyUnits.Count == 0)
            {
                // no enemies nearby
                if(NPC.anim.GetBool(_nearbyEnemy) != false) NPC.anim.SetBool(_nearbyEnemy, false);
                // cannot be in melee range of enemy units since there are none
                if(NPC.minionType == MinionType.Melee) if (NPC.anim.GetBool(_inMeleeRange) != false) NPC.anim.SetBool(_inMeleeRange, false);
                // there is no enemy for this minion to target
                if(NPC.targetEnemy != null) NPC.targetEnemy = null;
                // no longer need to order enemies by distance
                if (NPC.functions.IsInvoking("OrderEnemies")) NPC.functions.CancelInvoke("OrderEnemies");
            }
            // else there is an enemy minion nearby
            else
            {
                // order nearby units by distance
                if(!NPC.functions.IsInvoking("OrderEnemies")) NPC.functions.InvokeRepeating("OrderEnemies",0f,0.3f);
                // there are enemy minions nearby
                if (NPC.anim.GetBool(_nearbyEnemy) != true) NPC.anim.SetBool(_nearbyEnemy, true);
                // minion will target the nearest enemy
                if(NPC.targetEnemy != NPC.nearbyEnemyUnits[0]) NPC.targetEnemy = NPC.nearbyEnemyUnits[0];
            }
            #endregion
            #region allies
            // if there are no nearby allies, set flag to false
            if (NPC.nearbyAllyUnits.Count == 0)
            {
                // set to false
                if(NPC.anim.GetBool(_nearbyAlly) != false) NPC.anim.SetBool(_nearbyAlly, false);
            }
            // else there is an ally minion nearby
            else
            {
                // there are nearby allies
                if (NPC.anim.GetBool(_nearbyAlly) != true) NPC.anim.SetBool(_nearbyAlly, true);
            }
            #endregion
        #endregion

        #region in range for melee attack
        // if there is a valid target
        if (NPC.minionType == MinionType.Melee)
        {
            if (NPC.targetEnemy != null)
            {
                // if target is in range
                if (Vector3.Distance(NPC.transform.position, NPC.targetEnemy.gameObject.transform.position) < NPC.meleeRadius)
                {
                    // set inMeleeRange to true
                    NPC.anim.SetBool(_inMeleeRange, true);
                }
                // otherwise set false
                else NPC.anim.SetBool(_inMeleeRange, false);
            }
            // no valid target; set false
            else NPC.anim.SetBool(_inMeleeRange, false);
        }
        #endregion

        #region critical health
        // if minion's health is in critical health, set the flag in the animator
        if (NPC.health / NPC.maxHealth < 0.2f)
        {
            // set true
            NPC.anim.SetBool(_criticalHealth, true);
        }
        // else set false
        else NPC.anim.SetBool(_criticalHealth, false);
        #endregion

        #region healer's heal
        // if this minion is a healer
        if(NPC.minionType == MinionType.Healer)
        {
            // healers have self-regenerating hp
            NPC.health += (NPC.maxHealth / 20f) * Time.deltaTime;
            // if there are allies nearby
            if (NPC.nearbyAllyUnits.Count > 0)
            {
                // heal each ally
                foreach (GameObject minion in NPC.nearbyAllyUnits)
                {
                    minion.GetComponent<BaseVariables>().health += (minion.GetComponent<BaseVariables>().maxHealth / 20f) * Time.deltaTime;
                }
            }
        }
        #endregion

        #region clamp health & kill this minion if hp = 0
        NPC.health = Mathf.Clamp(NPC.health, 0f, NPC.maxHealth);
        if (NPC.health == 0f) NPC.anim.SetBool(_isDead, true);
        #endregion
    }
    #endregion
    #region OnStateExit
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    { 
    }
    #endregion
}
