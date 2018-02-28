using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfUnits : MonoBehaviour
{
    public static GameObject[] units;

    void Awake()
    {
        units = GameObject.FindGameObjectsWithTag("Unit");
    }
}
