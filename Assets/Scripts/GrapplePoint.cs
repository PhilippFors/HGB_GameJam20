using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GrapplePoint : MonoBehaviour
{
    public bool active;
    public Material mat;

    public Color inactiveC;
    public Color activeC;

    private void Start()
    {
        SetInactive();
    }
    public void SetActive()
    {
        active = true;

        mat.SetColor("_EmissiveColor", activeC);
    }

    public void SetInactive()
    {
        active = false;
        mat.SetColor("_EmissiveColor", inactiveC);
    }
}

