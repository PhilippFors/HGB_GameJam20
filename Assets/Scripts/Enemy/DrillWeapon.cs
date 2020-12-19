using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth p = other.gameObject.GetComponent<PlayerHealth>();
        if(p != null)
            p.Damage();
    }   
}
