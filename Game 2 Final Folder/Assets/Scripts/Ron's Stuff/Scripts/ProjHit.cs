using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjHit : MonoBehaviour {


    public MinionSide side;
    public AudioClip hitEnemySound;

    private AudioSource source;

    float time = 0f;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

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
                source.PlayOneShot(hitEnemySound);
                other.GetComponent<BaseVariables>().health -= 25f;
            }
        }
    }
}
