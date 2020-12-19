using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionVision : MonoBehaviour
{
    public MeshRenderer screen;
    public RenderTexture viewTexture;
    public Camera secondaryCamera;
    MeshFilter screenMeshFilter;

    void Awake()
    {
        screenMeshFilter = screen.GetComponent<MeshFilter>();
        CreateViewTexture();

        screen.material.SetInt("displayMask", 1);
        screen.material.SetTexture("_MainTex", viewTexture);
    }

    void CreateViewTexture()
    {
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            if (viewTexture != null)
            {
                viewTexture.Release();
            }
            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);

            secondaryCamera.targetTexture = viewTexture;
        }
    }
}
