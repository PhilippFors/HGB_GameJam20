using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePart : Hideable
{
    public BoxCollider boxCol;
    public MeshRenderer rend;
    private void Start()
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
    public override void Hide()
    {
        if (rend != null)
            rend.enabled = false;
        boxCol.enabled = false;
        isHidden = true;
    }

    public override void Unhide()
    {
        if (rend != null)
            rend.enabled = true;
        boxCol.enabled = true;
        isHidden = false;
    }

}
