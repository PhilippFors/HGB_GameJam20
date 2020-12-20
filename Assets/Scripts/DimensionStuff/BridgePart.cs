using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePart : Hideable
{
    private void Start()
    {
        INIT();
    }
    private void Update()
    {
        DoRayCast();
    }
    public override void Hide()
    {
        if (exists && rend != null)
            rend.enabled = false;

        boxCol.enabled = false;

        isHidden = true;
        if (copy != null)
            copy.GetComponent<MeshRenderer>().enabled = false;
    }

    public override void Unhide()
    {
        if (exists && rend != null)
            rend.enabled = true;
        boxCol.enabled = true;
        isHidden = false;
        if (copy != null)
            copy.GetComponent<MeshRenderer>().enabled = true;
    }

}
