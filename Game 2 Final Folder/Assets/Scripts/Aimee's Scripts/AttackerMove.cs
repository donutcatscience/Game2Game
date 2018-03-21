using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerMove : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float walkSpeed;

    [Tooltip ("Average number of seconds btwn appearances")]
    public float seenEverySeconds;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
    }
}