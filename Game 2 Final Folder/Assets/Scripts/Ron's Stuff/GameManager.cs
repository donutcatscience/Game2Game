using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Texture[] tex;

    // each minion has an animator controller; it will be set based on minion type
    public RuntimeAnimatorController[] animCont;


    public int[] maxHealth, damage;
    public float[] speed, meleeRadius, sphereRadius;


}
