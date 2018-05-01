using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

#region enums
// determines which side the minions are on
public enum MinionSide { Black, White}
// determines which class the minion is in
public enum MinionType { Melee, Ranged, Healer, Player}
#endregion

public class BaseVariables : MonoBehaviour
{
    #region variable declarations
    // which side the minion is on
    public MinionSide minionSide;
    // which class the minion is in
    public MinionType minionType;

    // maximum health the minion has
    public float maxHealth;
    // current health the minion has
    public float health;
    // amount of damage the minion deals to enemy minions
    public float damage;
    // movement speed of the minion
    public float speed;
    // the radius of the minion's minion range
    public float meleeRadius;

    // the enemy minion that this minion is targeting
    public GameObject targetEnemy;
    // the enemy minion that this minion is targeting
    public GameObject targetAlly;

    // is there an enemy minion nearby this minion?
    public bool isEnemyNearby;
    // is there an ally minion nearby this minion?
    public bool isAllyNearby;
    // is this minion dead?
    public bool isDead;
    // is this minion capturing an objective?
    public bool capturingObj = false;

    public bool criticalHealth = false;
    public bool inMeleeRange;
    public bool atDestination;
    public Animator anim;                             // animator of this minion
    public NavMeshAgent agent;                        // Navmesh agent of this minion
    [HideInInspector] public GameObject[] waypoint = new GameObject[2]; // list of waypoints
    public Renderer rend;                             // renderer component

    public Transform cannon;

    GameManager manager;

    public SphereCollider sphere;

    // each minion will need to be aware of enemy minions in aggro range
    public List<BaseVariables> nearbyEnemyUnits = new List<BaseVariables>();
    // each minion will need to be aware of ally minions in aggro range
    public List<BaseVariables> nearbyAllyUnits = new List<BaseVariables>();
    // each minion will need to be aware if they are being targeted by other enemy minions
    //public int targetedByUnits;





    #endregion

    void Start()
    {

        // minion will not be targeting any minions at the start
        targetEnemy = null;
        targetAlly = null;


        // these variables will update themselves
        isEnemyNearby = false;
        isAllyNearby = false;
       
        // assign waypoints
        waypoint[0] = GameObject.FindGameObjectWithTag("Waypoint B");
        waypoint[1] = GameObject.FindGameObjectWithTag("Waypoint W");
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        if (minionType != MinionType.Player)
        {

            int minionTypeInt = (int)minionType;
            if (minionSide == MinionSide.White)
                rend.material.SetTexture("_MainTex", manager.tex[minionTypeInt]);
            else if (minionSide == MinionSide.Black)
                rend.material.SetTexture("_MainTex", manager.tex[minionTypeInt + 3]);
            anim.runtimeAnimatorController = manager.animCont[minionTypeInt];
            damage = manager.damage[minionTypeInt];
            speed = manager.speed[minionTypeInt];
            meleeRadius = manager.meleeRadius[minionTypeInt];
            sphere.radius = manager.sphereRadius[minionTypeInt];
        }
        // minion's health will start at maximum
        health = maxHealth;

        isDead = false;

        NewSpawningCode.minionAmount++;
        print("SIZE: " + NewSpawningCode.minionAmount);
    }

    private void LateUpdate()
    {
        #region clamp health & kill this minion if hp = 0
        if (!isDead)
        {
            health = Mathf.Clamp(health, 0, maxHealth);
            if (health == 0) isDead = true;
        }
        #endregion
    }

    #region functions
    /// <summary>
    /// Set destination to nearest enemy
    /// </summary>
    void Follow()
    {
        if (targetEnemy != null) agent.SetDestination(targetEnemy.transform.position);
        print("calculating nearest path to enemy");
    }

    /// <summary>
    /// Removes dead minions from the list of minion references that is passed in
    /// </summary>
    /// <param name="list"></param>
    public void RemoveFromListWhenDead(List<BaseVariables> list)
    {
        foreach (BaseVariables minion in list.ToList())
        {
            if (minion.isDead)
            {
                list.Remove(minion);
            }
        }
    }

    /// <summary>
    /// Orders the minions in a given list by the distance from casting minion
    /// </summary>
    /// <param name="list"></param>
    public void OrderByDistance(List<BaseVariables> list)
    {
        // we only need to sort the order if there are more than 2 units in the list
        if (list.Count <= 1) return;
        // sorting algorithm
        print("sorting list");
        list.Sort(delegate (BaseVariables c1, BaseVariables c2)
        {
            return Vector3.Distance(transform.position, c1.transform.position).CompareTo((Vector3.Distance(transform.position, c2.transform.position)));
        });
    }

    public void OrderEnemies()
    {
        OrderByDistance(nearbyEnemyUnits);
    }
    public void OrderAllies()
    {
        OrderByDistance(nearbyAllyUnits);
    }

    public void Attack()
    {
        if (targetEnemy == null) return;
        if (minionType == MinionType.Melee)
        {
            targetEnemy.GetComponent<BaseVariables>().health -= damage;
        }
        Debug.Log("Attack");
    }


    public void HealerDestination()
    {
        // this vector will be the destination of this healer minion
        Vector3 center = Vector3.zero;
        // find out how many minions are in each list
        int countEnemy = nearbyEnemyUnits.Count();
        int countAlly = nearbyAllyUnits.Count();
        // if there are no units nearby, escape this function
        if (countAlly == 0)
        {
            print("NO ENEMIES");
            agent.SetDestination(waypoint[minionSide == MinionSide.White ? 0 : 1].transform.position);
            return;
        }

        foreach (BaseVariables minion in nearbyAllyUnits)
        {
            center += minion.transform.position;
        }


        // consider each enemy in range
        foreach (BaseVariables minion in nearbyEnemyUnits)
        {
            // calculate a position behind the player that lines up with the direction from this minion to the enemy unit
            Vector3 temp = transform.position + ((minion.transform.position - transform.position).normalized * -3);
            // add this vector to the center vector in order to offset the destination from enemies
            center += new Vector3(temp.x, minion.transform.position.y, temp.z);
        }
        // find the average of all positions accumulated
        center = center / (countEnemy + countAlly);
        agent.SetDestination(center);
        print("Going to average point");
    }


    void RangedAttack()
    {
        GetComponentInChildren<LaunchProjectile>().attack = true;
    }

    void Death()
    {
        NewSpawningCode.minionAmount--;
        // put the code for summoning resources here


        print("SIZE: " + NewSpawningCode.minionAmount);
        Destroy(gameObject);
    }

    #endregion
}
