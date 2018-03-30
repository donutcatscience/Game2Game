using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#region enums
// determines which side the minions are on
public enum MinionSide { Black, White}
// determines which class the minion is in
public enum MinionType { Melee, Ranged, Healer}
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
    // the radius of the minion's aggro (sphere of influence)
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

    // hide these from the inspector, since we will not be assigning or viewing these variables
    [HideInInspector] public Animator anim;                             // animator of this minion
    [HideInInspector] public NavMeshAgent agent;                        // Navmesh agent of this minion
    [HideInInspector] public MonoBehaviour functions;                   // script with functions
    [HideInInspector] public GameObject[] waypoint = new GameObject[2]; // list of waypoints

    // each minion will need to be aware of enemy minions in aggro range
    public List<GameObject> nearbyEnemyUnits = new List<GameObject>();
    // each minion will need to be aware of ally minions in aggro range
    public List<GameObject> nearbyAllyUnits = new List<GameObject>();
    // each minion will need to be aware if they are being targeted by other enemy minions
    public int targetedByUnits;
    #endregion

    void Awake()
    {
        // minion's maximum health
        maxHealth = Random.Range(15f, 25f);
        // minion's health will start at maximum
        health = maxHealth;

        // minion's damage dealt
        damage = Random.Range(3f, 8f);

        // minion will not be targeting any minions at the start
        targetEnemy = null;
        targetAlly = null;

        // assign component references
        anim = GetComponentInChildren<Animator>();
        agent = GetComponentInChildren<NavMeshAgent>();
        functions = GetComponent<Functions>();

        // these variables will update themselves
        isEnemyNearby = false;
        isAllyNearby = false;
       
        // assign waypoints
        waypoint[0] = GameObject.FindGameObjectWithTag("Waypoint B");
        waypoint[1] = GameObject.FindGameObjectWithTag("Waypoint W");

    }
}
