using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hideable : MonoBehaviour
{
    public bool exists;
    public bool isHidden;
    RaycastHit hit;
    public abstract void Hide();
    public abstract void Unhide();

    private void Update()
    {
        Ray ray = new Ray(transform.position, Camera.main.transform.position - transform.position);
        if (Physics.Raycast(ray, out hit, 30f))
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
