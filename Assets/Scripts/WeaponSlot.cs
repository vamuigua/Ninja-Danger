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

    //Drop the weapon in the weapon slot
    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "WeaponButton")
            {
                GameObject.Destroy(child.gameObject);
            }
            child.gameObject.SetActive(false);
        }
    }
}
