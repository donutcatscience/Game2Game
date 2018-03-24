using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MinionSide { Black, White}
public enum MinionType { Melee, Ranged, Healer}

public class BaseVariables : MonoBehaviour
{
    public MinionSide minionSide;
    public MinionType minionType;
    public float health;
    public float damage;
    public float speed;
    public float attackRadius;

    [HideInInspector] public Animator anim;

    public bool isMinionNearby;
    public bool isDead;

    [HideInInspector] public NavMeshAgent agent;

    public GameObject targetMinion;
    [HideInInspector] public GameObject[] waypoint = new GameObject[2];


    [HideInInspector] public MonoBehaviour coroutines = null;

    public List<GameObject> nearbyEnemyUnits = new List<GameObject>();
    public List<GameObject> nearbyAllyUnits = new List<GameObject>();
    public List<GameObject> targetedByUnits = new List<GameObject>();

    void Awake()
    {
        health = Random.Range(15f, 25f);
        damage = Random.Range(3f, 8f);
        targetMinion = null;
        anim = GetComponentInChildren<Animator>();
        isMinionNearby = false;
        agent = GetComponentInChildren<NavMeshAgent>();
        coroutines = GetComponent<Functions>();
        anim.SetInteger("minionType", (int)minionType);
        waypoint[0] = GameObject.FindGameObjectWithTag("Waypoint B");
        waypoint[1] = GameObject.FindGameObjectWithTag("Waypoint W");

    }
}
