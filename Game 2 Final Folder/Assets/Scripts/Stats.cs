using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public float HealthPoints;
    public float AttackPower;

    void Update()
    {
        if (HealthPoints <= 0) Destroy(this.gameObject);
    }
}
