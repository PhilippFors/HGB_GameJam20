using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HideEnemy : MonoBehaviour
{
    public bool isDisabled;
    public Transform[] rayEmitters;
    public abstract void Disable();
    int i = 0;
    public abstract void Enable();
    public LayerMask dimension;
    public void DoRayCast()
    {
        RaycastHit hit;
        i = 0;
        foreach (Transform raye in rayEmitters)
        {
            Ray ray = new Ray(raye.position, Camera.main.transform.position - raye.position);
            if (Physics.Raycast(ray, out hit, 20f, dimension))
            {
                if (hit.transform.GetComponent<DimensionVision>())
                {
                    i++;
                }
            }
        }
        if (i == 2)
            Disable();
        else
            Enable();
    }

    public void Damage()
    {
        Destroy(gameObject);
    }
}
