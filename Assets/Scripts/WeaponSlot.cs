using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    [HideInInspector]
    public bool empty = true;

    void Start()
    {
        empty = true;
    }
}
