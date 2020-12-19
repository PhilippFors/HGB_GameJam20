using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePart : Hideable
{
    public BoxCollider boxCol;

    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
    }
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

}
