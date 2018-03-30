using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMode : MonoBehaviour {

    //general sword stats
    public AudioClip hitSolid;
    public AudioClip hitEnemy;
    public GameObject impactEffect;
    public float impactDestroyTime;
    public float swingTimer = 0.25f;

    //sword swing #1
    public AudioClip swingOneSound;
    public GameObject swingOneParticle;
    public float swingOneParticleDestroyTimer;
    public float damage = 50f;

    //sword swing #2
    public AudioClip swingTwoSound;
    public GameObject swingTwoParticle;
    public float swingTwoParticleDestroyTimer;
    public float swingTwoDamage = 75f;

    //sword swing #3
    public AudioClip swingThreeSound;
    public GameObject swingThreeParticle;
    public float swingThreeParticleDestroyTimer;
    public float swingThreeDamage = 25f;


    private AudioSource source;
    private Animator animator;
    private float currentTime;
    private int comboCount = 0;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        //third swing
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift)
             && comboCount == 2 && (Time.time < currentTime + swingTimer))
        {
            PerformAttackThree();
            comboCount = 0;
        }
        else if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift)
             && comboCount == 1 && (Time.time < currentTime + swingTimer))
        {
            PerformAttackTwo();
            currentTime = Time.time;
            comboCount = 2;
        }
        else if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift) && comboCount == 0)
        {
            PerformAttack();
            currentTime = Time.time;
            comboCount = 1;
        }
        else if ((Time.time > currentTime + swingTimer) && (comboCount >= 0))
        {
            comboCount = 0;
            ReturnToIdle();
        }

    }

    public void PerformAttack()
    {
        animator.SetTrigger("BaseAttack");
        source.PlayOneShot(swingOneSound);
        GameObject swingParticle = Instantiate(swingOneParticle, GetComponent<SwordMode>().transform.position,
            Quaternion.LookRotation(GetComponent<SwordMode>().transform.position));
        Destroy(swingParticle, swingOneParticleDestroyTimer);
    }

    public void PerformAttackTwo()
    {
        animator.SetTrigger("SecondSwing");
        source.PlayOneShot(swingTwoSound);
    }

    public void PerformAttackThree()
    {
        animator.SetTrigger("ThirdSwing");
        source.PlayOneShot(swingThreeSound);
    }

    public void ReturnToIdle()
    {
        animator.SetTrigger("Idle");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            source.PlayOneShot(hitEnemy);

            Enemy target = other.transform.GetComponent<Enemy>();
            target.TakeDamage(damage);

            //Swing particle Effects Coming Soon
            //GameObject impactGo = Instantiate(impactEffect, other.transform.position, Quaternion.LookRotation(other.transform.position));
            //Destroy(impactGo, impactDestroyTime);
           // Destroy(gameObject, 0.25f);
        }
    }
}
