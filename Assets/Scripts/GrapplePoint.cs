using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class GrapplePoint : Hideable
{
    public bool active;
    public Material mat;

    public Color inactiveC;
    public Color activeC;
    Sequence tweenSeq;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        SetInactive();
        base.INIT();
    }
    private void Update()
    {
        DoRayCast();
    }
    public void SetActive()
    {
        active = true;
        mat.DOFloat(0.95f, "_EmissiveExposureWeight", 0.3f);
        // mat.DOFloat(1f, "_EmissiveExposureWeight", 0.3f);
        if (copy != null)
            copy.GetComponent<MeshRenderer>().material.DOFloat(0.95f, "_EmissiveExposureWeight", 0.3f);
    }

    public void SetInactive()
    {
        active = false;

        mat.DOFloat(1f, "_EmissiveExposureWeight", 0.3f);
        if (copy != null)
            copy.GetComponent<MeshRenderer>().material.DOFloat(1f, "_EmissiveExposureWeight", 0.3f);
    }

    public override void Hide()
    {
        rend.enabled = false;
        boxCol.enabled = false;
    }

    public override void Unhide()
    {
        // rend.enabled = true;
        boxCol.enabled = true;
    }
}

