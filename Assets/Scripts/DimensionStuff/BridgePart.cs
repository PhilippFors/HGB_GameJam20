using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePart : Hideable
{
    public BoxCollider boxCol;
    public override void Hide()
    {
        boxCol.enabled = false;
        isHidden = true;
    }

    public override void Unhide()
    {
        boxCol.enabled = true;
        isHidden = false;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.GetComponent<DimensionTrigger>())
    //         Unhide();
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.GetComponent<DimensionTrigger>())
    //         Hide();
    // }
}
