using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : Triggers
{
    public override void Start()
    {
        base.Start();
        gameObject.GetComponent<SphereCollider>().radius = NPC.meleeRadius;
    }

    void OnTriggerEnter(Collider other)
    {
        NPC.anim.SetBool("inMeleeRange", true);
    }
    void OnTriggerExit(Collider other)
    {
        NPC.anim.SetBool("inMeleeRange", false);
    }
}
