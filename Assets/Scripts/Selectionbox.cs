using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectionbox : MonoBehaviour
{
    public InputManager inputManager;
    public Transform selectionBox;
    Vector2 startPos;
    // Start is called before the first frame update

    bool isClicking = false;
    bool isHoldingDown = false;
    //To test the square's corners
    // public Transform sphere1;
    // public Transform sphere2;
    // public Transform sphere3;
    // public Transform sphere4;
    //The materials

    //To determine if we are clicking with left mouse or holding down left mouse
    // float delay = 0.3f;
    // float clickTime = 0f;
    //The start and end coordinates of the square we are making
    Vector3 squareStartPos;
    Vector3 squareEndPos;
    //If it was possible to create a square
    bool hasCreatedSquare;
    //The selection squares 4 corner positions
    Vector3 TL, TR, BL, BR;
    public MeshFilter f;
    Plane zeplane;

    public float planeDist;
    void Start()
    {
        inputManager.inputControls.Gameplay.LeftButtonPress.started += ctx => ButtonStart();

        // inputManager.inputControls.Gameplay.LeftButtonPress.canceled += ctx => ButtonStop();
        selectionBox.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + planeDist);
        selectionBox.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (UnityEngine.Input.GetMouseButton(0))
            PullPlane();
    }

    void PullPlane()
    {

        if (!selectionBox.gameObject.activeInHierarchy)
        {
            selectionBox.gameObject.SetActive(true);
        }
        selectionBox.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + planeDist);
        zeplane = new Plane(Vector3.forward, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + planeDist));
        Ray cameraRay = Camera.main.ScreenPointToRay(inputManager.mousePos);
        float rayLength;
        if (zeplane.Raycast(cameraRay, out rayLength))
        {
            squareEndPos = cameraRay.GetPoint(rayLength);
        }

        Vector3[] verts = f.mesh.vertices;

        // if (selectionBox.transform.position.x >= 0)
        // {
        if (squareEndPos.x <= squareStartPos.x & squareEndPos.y <= squareStartPos.y)
        {
            verts[0] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareEndPos.y, verts[0].z);
            verts[3] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareEndPos.y, verts[3].z);
            verts[1] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareStartPos.y, verts[1].z);
            verts[2] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareStartPos.y, verts[2].z);
        }
        else if (squareEndPos.x >= squareStartPos.x & squareEndPos.y >= squareStartPos.y)
        {
            verts[0] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareEndPos.y, verts[0].z);
            verts[3] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareEndPos.y, verts[3].z);
            verts[1] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareStartPos.y, verts[1].z);
            verts[2] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareStartPos.y, verts[2].z);
        }
        else
        {
            verts[2] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareEndPos.y, verts[2].z);
            verts[3] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareEndPos.y, verts[3].z);
            verts[1] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareStartPos.y, verts[1].z);
            verts[0] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareStartPos.y, verts[0].z);
        }

        // else
        // {
        //     if (squareEndPos.x <= squareStartPos.x & squareEndPos.y <= squareStartPos.y)
        //     {
        //         verts[0] = new Vector3(squareEndPos.x + selectionBox.transform.position.x, squareEndPos.y, verts[0].z);
        //         verts[3] = new Vector3(squareStartPos.x + selectionBox.transform.position.x, squareEndPos.y, verts[3].z);
        //         verts[1] = new Vector3(squareEndPos.x + selectionBox.transform.position.x, squareStartPos.y, verts[1].z);
        //         verts[2] = new Vector3(squareStartPos.x + selectionBox.transform.position.x, squareStartPos.y, verts[2].z);
        //     }
        //     else if (squareEndPos.x >= squareStartPos.x & squareEndPos.y >= squareStartPos.y)
        //     {
        //         verts[0] = new Vector3(squareEndPos.x + selectionBox.transform.position.x, squareEndPos.y, verts[0].z);
        //         verts[3] = new Vector3(squareStartPos.x + selectionBox.transform.position.x, squareEndPos.y, verts[3].z);
        //         verts[1] = new Vector3(squareEndPos.x + selectionBox.transform.position.x, squareStartPos.y, verts[1].z);
        //         verts[2] = new Vector3(squareStartPos.x + selectionBox.transform.position.x, squareStartPos.y, verts[2].z);
        //     }
        //     else
        //     {
        //         verts[2] = new Vector3(squareEndPos.x + selectionBox.transform.position.x, squareEndPos.y, verts[2].z);
        //         verts[3] = new Vector3(squareStartPos.x + selectionBox.transform.position.x, squareEndPos.y, verts[3].z);
        //         verts[1] = new Vector3(squareEndPos.x + selectionBox.transform.position.x, squareStartPos.y, verts[1].z);
        //         verts[0] = new Vector3(squareStartPos.x + selectionBox.transform.position.x, squareStartPos.y, verts[0].z);
        //     }
        // }
        f.mesh.vertices = verts;
    }

    private void OnDrawGizmos()
    {
        Vector3[] verts = f.sharedMesh.vertices;
        float i = 0.1f;
        for (int x = 0; x < verts.Length; x++, i += 0.1f)
        {
            Gizmos.DrawSphere(verts[x], i);
        }

    }
    void ButtonStart()
    {
        selectionBox.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + planeDist);
        zeplane = new Plane(Vector3.forward, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + planeDist));
        Ray cameraRay = Camera.main.ScreenPointToRay(inputManager.mousePos);
        float rayLength;
        if (zeplane.Raycast(cameraRay, out rayLength))
        {
            squareStartPos = cameraRay.GetPoint(rayLength);
            // selectionBox.position = squareStartPos;
        }
    }

    void ButtonStop()
    {

        //Deactivate the square selection image
        selectionBox.gameObject.SetActive(false);

        //Clear the list with selected unit

    }
    // //Select units with click or by draging the mouse
}
