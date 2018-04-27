using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour {



    public GameObject projectile;
    public float shootForce = 2000f;
    public bool attack = false;
    public GameObject enemy;
    public BaseVariables variables;
    public Transform offset;

    private void Start()
    {
        InvokeRepeating("GetEnemy", 0f, 0.5f);
    }

    void Update ()
    {

        if (enemy != null) transform.LookAt(enemy.transform);
            //transform.rotation.SetLookRotation(enemy.transform.position - transform.position);
        if (attack)
        {
            GameObject proj = Instantiate(projectile, transform.position, offset.transform.rotation);
            proj.GetComponent<Rigidbody>().AddForce(proj.transform.forward * shootForce);
            proj.GetComponent<ProjHit>().side = variables.minionSide;
            attack = false;
        }

	}

    void GetEnemy()
    {
        enemy = GetComponentInParent<BaseVariables>().targetEnemy;
    }
}
