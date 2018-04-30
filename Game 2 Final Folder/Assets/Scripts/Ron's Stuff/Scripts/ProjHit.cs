using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjHit : MonoBehaviour {


    public MinionSide side;

    float time = 0f;

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= 3f)
        {
            Destroy(this.gameObject);
        }
    }

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
    }
}
