using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hideable : MonoBehaviour
{
    public bool isHidden;
    RaycastHit hit;
    public abstract void Hide();
    public abstract void Unhide();

    private void Update()
    {
        Ray ray = new Ray(transform.position, Camera.main.transform.position - transform.position);
        if (Physics.Raycast(ray, out hit, 50f))
        {
            if (hit.transform.GetComponent<DimensionVision>())
                Unhide();
        }
        else
        {
            Hide();
        }

    }
}
