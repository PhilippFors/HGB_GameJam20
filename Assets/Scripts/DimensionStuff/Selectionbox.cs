using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectionbox : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] Transform selectionBox;
    // public 
    Vector3 squareStartPos;
    Vector3 squareEndPos;
    Vector3 lastPos;
    [SerializeField] MeshFilter f;
    Plane zeplane;

    [SerializeField] BoxCollider boxCol;
    [SerializeField] BoxCollider screenBoxCol;
    [SerializeField] float planeDist;
    public DimensionTrigger collection;

    public bool selecting;

    public float windowSize;

    void Start()
    {
        inputManager.inputControls.Gameplay.LeftButtonPress.started += ctx => ButtonStart();

        inputManager.inputControls.Gameplay.LeftButtonPress.canceled += ctx => ButtonStop();

        inputManager.inputControls.Gameplay.RightButton.performed += ctx => CloseBox();
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
            boxCol.gameObject.SetActive(true);
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
        float z = 0;

        if (Vector3.Distance(squareStartPos, squareEndPos) <= windowSize)
        {

            lastPos = (squareEndPos + squareStartPos) / 2f;

            z = boxCol.transform.position.z;

            boxCol.transform.position = lastPos;

            boxCol.transform.position = new Vector3(boxCol.transform.position.x, boxCol.transform.position.y, z);

            z = screenBoxCol.transform.position.z;

            screenBoxCol.transform.position = lastPos;

            screenBoxCol.transform.position = new Vector3(screenBoxCol.transform.position.x, screenBoxCol.transform.position.y, selectionBox.transform.position.z);

            Vector3[] verts = f.mesh.vertices;

            if (squareEndPos.x <= squareStartPos.x & squareEndPos.y <= squareStartPos.y)
            {
                verts[0] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareEndPos.y - selectionBox.transform.position.y, verts[0].z);
                verts[3] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareEndPos.y - selectionBox.transform.position.y, verts[3].z);
                verts[1] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareStartPos.y - selectionBox.transform.position.y, verts[1].z);
                verts[2] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareStartPos.y - selectionBox.transform.position.y, verts[2].z);
                boxCol.size = new Vector3(Vector3.Distance(verts[0], verts[3]), Vector3.Distance(verts[0], verts[1]), boxCol.size.z);
                screenBoxCol.size = new Vector3(Vector3.Distance(verts[0], verts[3]), Vector3.Distance(verts[0], verts[1]), screenBoxCol.size.z);
            }
            else if (squareEndPos.x >= squareStartPos.x & squareEndPos.y >= squareStartPos.y)
            {
                verts[0] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareEndPos.y - selectionBox.transform.position.y, verts[0].z);
                verts[3] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareEndPos.y - selectionBox.transform.position.y, verts[3].z);
                verts[1] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareStartPos.y - selectionBox.transform.position.y, verts[1].z);
                verts[2] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareStartPos.y - selectionBox.transform.position.y, verts[2].z);
                boxCol.size = new Vector3(Vector3.Distance(verts[0], verts[3]), Vector3.Distance(verts[0], verts[1]), boxCol.size.z);
                screenBoxCol.size = new Vector3(Vector3.Distance(verts[0], verts[3]), Vector3.Distance(verts[0], verts[1]), screenBoxCol.size.z);
            }
            else
            {
                verts[2] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareEndPos.y - selectionBox.transform.position.y, verts[2].z);
                verts[3] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareEndPos.y - selectionBox.transform.position.y, verts[3].z);
                verts[1] = new Vector3(squareEndPos.x - selectionBox.transform.position.x, squareStartPos.y - selectionBox.transform.position.y, verts[1].z);
                verts[0] = new Vector3(squareStartPos.x - selectionBox.transform.position.x, squareStartPos.y - selectionBox.transform.position.y, verts[0].z);
                boxCol.size = new Vector3(Vector3.Distance(verts[2], verts[3]), Vector3.Distance(verts[2], verts[1]), boxCol.size.z);
                screenBoxCol.size = new Vector3(Vector3.Distance(verts[0], verts[1]), Vector3.Distance(verts[0], verts[3]), screenBoxCol.size.z);
            }
            f.mesh.vertices = verts;
        }

        z = boxCol.transform.position.z;
        boxCol.transform.position = lastPos;

        boxCol.transform.position = new Vector3(boxCol.transform.position.x, boxCol.transform.position.y, z);

        z = screenBoxCol.transform.position.z;

        screenBoxCol.transform.position = lastPos;

        screenBoxCol.transform.position = new Vector3(screenBoxCol.transform.position.x, screenBoxCol.transform.position.y, selectionBox.transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + planeDist), 0.5f);
    }

    void ButtonStart()
    {
        selecting = true;
        selectionBox.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + planeDist);
        zeplane = new Plane(Vector3.forward, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + planeDist));
        Ray cameraRay = Camera.main.ScreenPointToRay(inputManager.mousePos);
        float rayLength;
        if (zeplane.Raycast(cameraRay, out rayLength))
        {
            squareStartPos = cameraRay.GetPoint(rayLength);
        }
        selecting = true;
    }

    void ButtonStop()
    {
        Debug.Log("Stop");

        if (squareEndPos.x >= squareStartPos.x)
        {
            collection.ReleaserToLeft();
        }
        else if (squareEndPos.x <= squareStartPos.x)
        {
            collection.ReleaseToRight();
        }
        // collection.Release();
        collection.QueueWorker();
        selecting = false;
    }

    void CloseBox()
    {
        selectionBox.gameObject.SetActive(false);
        boxCol.gameObject.SetActive(false);
    }

}
