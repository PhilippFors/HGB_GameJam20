using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectionbox : MonoBehaviour
{
    public InputManager inputManager;
    public Transform selectionBox;

    Vector3 squareStartPos;
    Vector3 squareEndPos;

    public MeshFilter f;
    Plane zeplane;

    public BoxCollider boxCol;

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

        // boxCol.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, boxCol.transform.position.z);
        selectionBox.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + planeDist);
        zeplane = new Plane(Vector3.forward, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + planeDist));
        Ray cameraRay = Camera.main.ScreenPointToRay(inputManager.mousePos);
        float rayLength;
        if (zeplane.Raycast(cameraRay, out rayLength))
        {
            squareEndPos = cameraRay.GetPoint(rayLength);
        }
        float z = boxCol.transform.position.z;

        boxCol.transform.position = (squareEndPos + squareStartPos) / 2f;

        boxCol.transform.position = new Vector3(boxCol.transform.position.x, boxCol.transform.position.y, z);

        Vector3[] verts = f.mesh.vertices;

        if (squareEndPos.x <= squareStartPos.x & squareEndPos.y <= squareStartPos.y)
        {
            verts[0] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareEndPos.y - selectionBox.transform.position.y, verts[0].z);
            verts[3] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareEndPos.y - selectionBox.transform.position.y, verts[3].z);
            verts[1] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareStartPos.y - selectionBox.transform.position.y, verts[1].z);
            verts[2] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareStartPos.y - selectionBox.transform.position.y, verts[2].z);
            boxCol.size = new Vector3(Vector3.Distance(verts[0], verts[3]), Vector3.Distance(verts[0], verts[1]), boxCol.size.z);
        }
        else if (squareEndPos.x >= squareStartPos.x & squareEndPos.y >= squareStartPos.y)
        {
            verts[0] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareEndPos.y - selectionBox.transform.position.y, verts[0].z);
            verts[3] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareEndPos.y - selectionBox.transform.position.y, verts[3].z);
            verts[1] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareStartPos.y - selectionBox.transform.position.y, verts[1].z);
            verts[2] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareStartPos.y - selectionBox.transform.position.y, verts[2].z);
            boxCol.size = new Vector3(Vector3.Distance(verts[0], verts[3]), Vector3.Distance(verts[0], verts[1]), boxCol.size.z);
        }
        else
        {
            verts[2] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareEndPos.y - selectionBox.transform.position.y, verts[2].z);
            verts[3] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareEndPos.y - selectionBox.transform.position.y, verts[3].z);
            verts[1] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareStartPos.y - selectionBox.transform.position.y, verts[1].z);
            verts[0] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareStartPos.y - selectionBox.transform.position.y, verts[0].z);
            boxCol.size = new Vector3(Vector3.Distance(verts[2], verts[3]), Vector3.Distance(verts[2], verts[1]), boxCol.size.z);
        }


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
        }
    }

    void ButtonStop()
    {
        selectionBox.gameObject.SetActive(false);
    }

}
