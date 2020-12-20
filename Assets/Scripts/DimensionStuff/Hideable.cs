using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hideable : MonoBehaviour
{
    public bool exists;
    public bool isHidden;
    public GameObject copy;
    public BoxCollider boxCol;
    public MeshRenderer rend;
    RaycastHit hit;

    public virtual void INIT()
    {
        rend = GetComponent<MeshRenderer>();
        boxCol = GetComponent<BoxCollider>();
        if (exists)
        {
            rend.enabled = true;
            boxCol.enabled = true;
        }
        else
        {
            rend.enabled = false;
            boxCol.enabled = false;
        }
    }

    public abstract void Hide();
    public abstract void Unhide();

    public void DoRayCast()
    {
        Ray ray = new Ray(transform.position, Camera.main.transform.position - transform.position);
        if (Physics.Raycast(ray, out hit, 30f, LayerMask.GetMask("Dimension")))
        {
            if (hit.transform.GetComponent<DimensionVision>())
            {
                if (exists)
                    Hide();
                else
                    Unhide();
            }
        }
        else
        {
            if (exists)
                Unhide();
            else
                Hide();
        }
    }
}
