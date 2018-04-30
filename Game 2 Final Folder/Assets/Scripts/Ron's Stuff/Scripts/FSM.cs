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

    public int _nearbyEnemy = Animator.StringToHash("nearbyEnemy");
    public int _nearbyAlly = Animator.StringToHash("nearbyAlly");
    public int _inMeleeRange = Animator.StringToHash("inMeleeRange");
    public int _atDestination = Animator.StringToHash("atDestination");
    public int _isDead = Animator.StringToHash("isDead");
    #endregion


    void Awake()
    {
        // try to get NPC here
    }

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
            NPC.isEnemyNearby = false;
            // cannot be in melee range of enemy units since there are none
            if (NPC.minionType != MinionType.Healer) NPC.inMeleeRange = false;
            // there is no enemy for this minion to target
            if (NPC.targetEnemy != null) NPC.targetEnemy = null;
            // no longer need to order enemies by distance
            if (NPC.IsInvoking("OrderEnemies")) NPC.CancelInvoke("OrderEnemies");
        }
        // else there is an enemy minion nearby
        else
        {
            // order nearby units by distance
            if (!NPC.IsInvoking("OrderEnemies") && NPC.nearbyEnemyUnits.Count >= 2) NPC.InvokeRepeating("OrderEnemies", 0f, 0.3f);
            // there are enemy minions nearby
            NPC.isEnemyNearby = true;
            // minion will target the nearest enemy
            if (NPC.targetEnemy != NPC.nearbyEnemyUnits[0]) NPC.targetEnemy = NPC.nearbyEnemyUnits[0].gameObject;
        }
        #endregion
        #region allies
        // if there are no nearby allies, set flag to false
        if (NPC.nearbyAllyUnits.Count == 0) NPC.isAllyNearby = false;
        else NPC.isAllyNearby = true;
        #endregion
        #region both
        if (NPC.nearbyAllyUnits.Count > 0)
        {
            NPC.RemoveFromListWhenDead(NPC.nearbyAllyUnits);
        }
        if (NPC.nearbyEnemyUnits.Count > 0)
        {
            NPC.RemoveFromListWhenDead(NPC.nearbyEnemyUnits);
        }
        #endregion
        #endregion

        #region healer's heal
        // if this minion is a healer
        if(NPC.minionType == MinionType.Healer)
        {
            // healers have self-regenerating hp
            NPC.health += (NPC.maxHealth / 5f) * Time.deltaTime;
            // if there are allies nearby
            if (NPC.nearbyAllyUnits.Count > 0)
            {
                // heal each ally
                foreach (BaseVariables minion in NPC.nearbyAllyUnits)
                {
                    minion.GetComponent<BaseVariables>().health += (minion.GetComponent<BaseVariables>().maxHealth / 5f) * Time.deltaTime;
                }
            }
        }
        #endregion


        #region in range for melee attack
        // if there is a valid target
        if (NPC.minionType != MinionType.Healer)
        {
            if (NPC.targetEnemy == null)
            {
                NPC.inMeleeRange = false;
            }
            else
            {
                // if target is in range
                if (Vector3.Distance(NPC.transform.position, NPC.targetEnemy.gameObject.transform.position) < NPC.meleeRadius)
                {
                    // set inMeleeRange to true
                    NPC.inMeleeRange = true;
                }
                // otherwise set false
                else NPC.inMeleeRange = false;
            }
        }
        #endregion


        if (NPC.agent.velocity.magnitude <= 0.001f) NPC.atDestination = true;
        else NPC.atDestination = false;



        if(NPC.isEnemyNearby && NPC.anim.GetBool(_nearbyEnemy) != true) NPC.anim.SetBool(_nearbyEnemy, true);
        else if (!NPC.isEnemyNearby && NPC.anim.GetBool(_nearbyEnemy) != false) NPC.anim.SetBool(_nearbyEnemy, false);

        if(NPC.inMeleeRange && NPC.anim.GetBool(_inMeleeRange) != true) NPC.anim.SetBool(_inMeleeRange, true);
        else if (!NPC.inMeleeRange && NPC.anim.GetBool(_inMeleeRange) != false) NPC.anim.SetBool(_inMeleeRange, false);

        if (NPC.isAllyNearby && NPC.anim.GetBool(_nearbyAlly) != true) NPC.anim.SetBool(_nearbyAlly, true);
        else if (!NPC.isAllyNearby && NPC.anim.GetBool(_nearbyAlly) != false) NPC.anim.SetBool(_nearbyAlly, false);

        if (NPC.atDestination && NPC.anim.GetBool(_atDestination) != true) NPC.anim.SetBool(_atDestination, true);
        else if (!NPC.atDestination && NPC.anim.GetBool(_atDestination) != false) NPC.anim.SetBool(_atDestination, false);

        if (NPC.isDead && NPC.anim.GetBool(_isDead) != true) NPC.anim.SetBool(_isDead, true);
        else if (!NPC.isDead && NPC.anim.GetBool(_isDead) != false) NPC.anim.SetBool(_isDead, false);

    }
    #endregion
    #region OnStateExit
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    { 
    }
    #endregion
}
