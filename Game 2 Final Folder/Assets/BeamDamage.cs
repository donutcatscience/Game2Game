using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDamage : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == gameObject) return;

            other.GetComponent<BaseVariables>().health -= 2f;
    }
}
