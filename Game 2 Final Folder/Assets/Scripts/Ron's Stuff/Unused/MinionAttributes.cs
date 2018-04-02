/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AttackType {Melee, Ranged, Hybrid}

public struct Attributes
{
    private float _speed;
    private float _health;
    private float _damage;
    private AttackType _attackType;

    public float speed { get { return _speed; } }
    public float health { get { return _health; } set { _health = value; } }
    public float damage { get { return _damage; } }
    public AttackType attackType { get { return _attackType; } set { _attackType = value; } }

    public Attributes Set(float s, float h, float d, AttackType at)
    {
        Attributes tempAttributes;
        tempAttributes._speed = s;
        tempAttributes._health = h;
        tempAttributes._damage = d;
        tempAttributes._attackType = at;

        return tempAttributes;
    }
}



public class MinionAttributes : MonoBehaviour
{
    #region Statics
    private static MinionAttributes _instance = null;
    public static MinionAttributes instance
    {
        get
        {
            if (_instance == null)
                _instance = (MinionAttributes)FindObjectOfType(typeof(MinionAttributes));
            return _instance;
        }
    }
    #endregion

    public Attributes[] attr = new Attributes[2];

    [Header("Class 1")]
    [SerializeField] public float s1;
    [SerializeField] public float h1;
    [SerializeField] public float d1;
    [SerializeField] public AttackType at1;

    [Header("Class 2")]
    [SerializeField] float s2;
    [SerializeField] float h2;
    [SerializeField] float d2;
    [SerializeField] AttackType at2;


    private void Awake()
    {
        attr[0] = attr[0].Set(s1,h1,d1,at1);
        attr[1] = attr[0].Set(s2, h2, d2, at2);
    }



}
*/