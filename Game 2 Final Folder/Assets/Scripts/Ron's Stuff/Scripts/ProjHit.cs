using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjHit : MonoBehaviour {


    public MinionSide side;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Minion"))
        {
            if (other.GetComponent<BaseVariables>().minionSide != side)
            {
                print("hit minion");
                other.GetComponent<BaseVariables>().health -= 50f;
            }
        }
        else print("hit ground");
        Destroy(gameObject);
    }
}
