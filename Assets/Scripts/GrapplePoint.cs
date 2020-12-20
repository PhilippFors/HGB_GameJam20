using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class GrapplePoint : MonoBehaviour
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
    }
    public void SetActive()
    {
        active = true;

       
     
        mat.DOFloat(0.95f, "_EmissiveExposureWeight", 0.3f);
    }

    public void SetInactive()
    {
        active = false;
        mat.DOFloat(1f, "_EmissiveExposureWeight", 0.3f);
    }
}

