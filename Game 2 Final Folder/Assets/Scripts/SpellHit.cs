using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellHit : MonoBehaviour
{

    public AudioClip hitSolid;
    public AudioClip hitEnemy;
    public GameObject impactEffect;
    public float impactDestroyTime;
    public float damage = 25f;

    private AudioSource source;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;
    private float velToVol = .2F;
    private float velocityClipSplit = 10F;
    private Vector3 hitLocation;


    void Awake()
    {
        impactDestroyTime = GetComponent<SpellHit>().impactDestroyTime;
        source = GetComponent<AudioSource>();

    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            //sound adjustments based on range
            source.pitch = Random.Range(lowPitchRange, highPitchRange);
            float hitVol = other.relativeVelocity.magnitude * velToVol;
            source.PlayOneShot(hitSolid, hitVol);
            GameObject impactGo = Instantiate(impactEffect, this.transform.position, Quaternion.LookRotation(other.transform.position));
            Destroy(impactGo, impactDestroyTime);
            Destroy(gameObject, 0.25f);

        } else if (other.gameObject.CompareTag("Enemy"))
            {
                //sound adjustments based on range
                source.pitch = Random.Range(lowPitchRange, highPitchRange);
                float hitVol = other.relativeVelocity.magnitude * velToVol;
                source.PlayOneShot(hitEnemy, hitVol);

                Enemy target = other.transform.GetComponent<Enemy>();
                target.TakeDamage(damage);

                GameObject impactGo = Instantiate(impactEffect, other.transform.position, Quaternion.LookRotation(other.transform.position));
                Destroy(impactGo, impactDestroyTime);
                Destroy(gameObject, 0.25f);
            }


    }
    private void OnTriggerEnter(Collider other)
    {
        //collects gems
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy target = other.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

}
