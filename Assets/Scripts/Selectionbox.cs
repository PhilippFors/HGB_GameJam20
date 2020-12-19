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

        inputManager.inputControls.Gameplay.LeftButtonPress.canceled += ctx => ButtonStop();
        selectionBox.transform.position = new Vector3(selectionBox.transform.position.x, selectionBox.transform.position.y, Camera.main.transform.position.z + planeDist);
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

        zeplane = new Plane(Vector3.forward, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + planeDist));
        Ray cameraRay = Camera.main.ScreenPointToRay(inputManager.mousePos);
        float rayLength;
        if (zeplane.Raycast(cameraRay, out rayLength))
        {
            squareEndPos = cameraRay.GetPoint(rayLength);
        }

        //Get the middle position of the square
        // Vector3 middle = (squareStartPos + squareEndPos) / 2f;
        // selectionBox.transform.position = middle;
        Vector3[] verts = f.mesh.vertices;

        // verts[2] = squareStartPos;
        // verts[1] = new Vector3(squareStartPos.x, squareEndPos.y, squareStartPos.z);
        // verts[3] = new Vector3(squareEndPos.x, squareStartPos.y, squareStartPos.z);
        // verts[0] = squareEndPos;


        if (squareEndPos.x <= squareStartPos.x & squareEndPos.y <= squareStartPos.y)
        {
            verts[0] = new Vector3(squareEndPos.x, squareEndPos.y, verts[0].z);
            verts[3] = new Vector3(squareStartPos.x, squareEndPos.y, verts[3].z);
            verts[1] = new Vector3(squareEndPos.x, squareStartPos.y, verts[1].z);
            verts[2] = new Vector3(squareStartPos.x, squareStartPos.y, verts[2].z);
        }
        else if (squareEndPos.x >= squareStartPos.x & squareEndPos.y >= squareStartPos.y)
        {
            verts[0] = new Vector3(squareEndPos.x, squareEndPos.y, verts[0].z);
            verts[3] = new Vector3(squareStartPos.x, squareEndPos.y, verts[3].z);
            verts[1] = new Vector3(squareEndPos.x, squareStartPos.y, verts[1].z);
            verts[2] = new Vector3(squareStartPos.x, squareStartPos.y, verts[2].z);
        }
        else
        {
            verts[2] = new Vector3(squareEndPos.x, squareEndPos.y, verts[2].z);
            verts[3] = new Vector3(squareStartPos.x, squareEndPos.y, verts[3].z);
            verts[1] = new Vector3(squareEndPos.x, squareStartPos.y, verts[1].z);
            verts[0] = new Vector3(squareStartPos.x, squareStartPos.y, verts[0].z);
        }

        f.mesh.vertices = verts;

        // // float dist = Vector3.Distance(squareStartPos, squareEndPos);
        // // selectionBox.localScale = new Vector3(dist, 1, dist);
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
        isClicking = true;
        isHoldingDown = false;

        //Select all units within the square if we have created a square
        if (hasCreatedSquare)
        {
            hasCreatedSquare = false;

            //Deactivate the square selection image
            selectionBox.gameObject.SetActive(false);

            //Clear the list with selected unit
        }
    }
    //Select units with click or by draging the mouse
    void SelectUnits()
    {
        //Are we clicking with left mouse or holding down left mouse
        // //Release the mouse button


        // }
        //Holding down the mouse button
        if (UnityEngine.Input.GetMouseButton(0))
        {





            //Select one unit with left mouse and deselect all units with left mouse by clicking on what's not a unit
            // if (isClicking)
            // {
            //     Deselect all units
            //     for (int i = 0; i < selectedUnits.Count; i++)
            //     {
            //         selectedUnits[i].GetComponent<MeshRenderer>().material = normalMaterial;
            //     }

            //     Clear the list with selected units
            //     selectedUnits.Clear();

            //     Try to select a new unit
            //     RaycastHit hit;
            //     Fire ray from camera
            //     if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200f))
            //     {
            //         Did we hit a friendly unit?
            //         if (hit.collider.CompareTag("Friendly"))
            //         {
            //             GameObject activeUnit = hit.collider.gameObject;
            //             Set this unit to selected
            //             activeUnit.GetComponent<MeshRenderer>().material = selectedMaterial;
            //             Add it to the list of selected units, which is now just 1 unit
            //             selectedUnits.Add(activeUnit);
            //         }
            //     }
            // }

            //Drag the mouse to select all units within the square

            //Activate the square selection image
            if (!selectionBox.gameObject.activeInHierarchy)
            {
                selectionBox.gameObject.SetActive(true);
            }

            //Get the latest coordinate of the square
            squareEndPos = inputManager.mousePos;

            //Display the selection with a GUI image
            DisplaySquare();

            //Highlight the units within the selection square, but don't select the units
            if (hasCreatedSquare)
            {
                // for (int i = 0; i < allUnits.Length; i++)
                // {
                //     GameObject currentUnit = allUnits[i];

                //     //Is this unit within the square
                //     if (IsWithinPolygon(currentUnit.transform.position))
                //     {
                //         currentUnit.GetComponent<MeshRenderer>().material = highlightMaterial;
                //     }
                //     //Otherwise deactivate
                //     else
                //     {
                //         currentUnit.GetComponent<MeshRenderer>().material = normalMaterial;
                //     }
                // }
            }
        }
    }

    //Highlight a unit when mouse is above it
    // void HighlightUnit()
    // {
    //     //Change material on the latest unit we highlighted
    //     if (highlightThisUnit != null)
    //     {
    //         //But make sure the unit we want to change material on is not selected
    //         bool isSelected = false;
    //         for (int i = 0; i < selectedUnits.Count; i++)
    //         {
    //             if (selectedUnits[i] == highlightThisUnit)
    //             {
    //                 isSelected = true;
    //                 break;
    //             }
    //         }

    //         if (!isSelected)
    //         {
    //             highlightThisUnit.GetComponent<MeshRenderer>().material = normalMaterial;
    //         }

    //         highlightThisUnit = null;
    //     }

    //     //Fire a ray from the mouse position to get the unit we want to highlight
    //     RaycastHit hit;
    //     //Fire ray from camera
    //     if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200f))
    //     {
    //         //Did we hit a friendly unit?
    //         if (hit.collider.CompareTag("Friendly"))
    //         {
    //             //Get the object we hit
    //             GameObject currentObj = hit.collider.gameObject;

    //             //Highlight this unit if it's not selected
    //             bool isSelected = false;
    //             for (int i = 0; i < selectedUnits.Count; i++)
    //             {
    //                 if (selectedUnits[i] == currentObj)
    //                 {
    //                     isSelected = true;
    //                     break;
    //                 }
    //             }

    //             if (!isSelected)
    //             {
    //                 highlightThisUnit = currentObj;

    //                 highlightThisUnit.GetComponent<MeshRenderer>().material = highlightMaterial;
    //             }
    //         }
    //     }
    // }

    // //Is a unit within a polygon determined by 4 corners
    // bool IsWithinPolygon(Vector3 unitPos)
    // {
    //     bool isWithinPolygon = false;

    //     //The polygon forms 2 triangles, so we need to check if a point is within any of the triangles
    //     //Triangle 1: TL - BL - TR
    //     if (IsWithinTriangle(unitPos, TL, BL, TR))
    //     {
    //         return true;
    //     }

    //     //Triangle 2: TR - BL - BR
    //     if (IsWithinTriangle(unitPos, TR, BL, BR))
    //     {
    //         return true;
    //     }

    //     return isWithinPolygon;
    // }

    //Is a point within a triangle
    //From http://totologic.blogspot.se/2014/01/accurate-point-in-triangle-test.html
    // bool IsWithinTriangle(Vector3 p, Vector3 p1, Vector3 p2, Vector3 p3)
    // {
    //     bool isWithinTriangle = false;

    //     //Need to set z -> y because of other coordinate system
    //     float denominator = ((p2.z - p3.z) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.z - p3.z));

    //     float a = ((p2.z - p3.z) * (p.x - p3.x) + (p3.x - p2.x) * (p.z - p3.z)) / denominator;
    //     float b = ((p3.z - p1.z) * (p.x - p3.x) + (p1.x - p3.x) * (p.z - p3.z)) / denominator;
    //     float c = 1 - a - b;

    //     //The point is within the triangle if 0 <= a <= 1 and 0 <= b <= 1 and 0 <= c <= 1
    //     if (a >= 0f && a <= 1f && b >= 0f && b <= 1f && c >= 0f && c <= 1f)
    //     {
    //         isWithinTriangle = true;
    //     }

    //     return isWithinTriangle;
    // }

    //Display the selection with a GUI square
    void DisplaySquare()
    {
        //The start position of the square is in 3d space, or the first coordinate will move
        //as we move the camera which is not what we want
        // Vector3 squareStartScreen = Camera.main.WorldToScreenPoint(squareStartPos);
        Vector3 squareStartScreen = squareStartPos;

        squareStartScreen.z = 0f;

        //Get the middle position of the square
        Vector3 middle = (squareStartScreen + squareEndPos) / 2f;

        //Set the middle position of the GUI square
        selectionBox.position = middle;

        //Change the size of the square
        float sizeX = Mathf.Abs(squareStartScreen.x - squareEndPos.x);
        float sizeY = Mathf.Abs(squareStartScreen.y - squareEndPos.y);

        //Set the size of the square
        // selectionBox.sizeDelta = new Vector2(sizeX, sizeY);

        //The problem is that the corners in the 2d square is not the same as in 3d space
        //To get corners, we have to fire a ray from the screen
        //We have 2 of the corner positions, but we don't know which,  
        //so we can figure it out or fire 4 raycasts
        TL = new Vector3(middle.x - sizeX / 2f, middle.y + sizeY / 2f, 0f);
        TR = new Vector3(middle.x + sizeX / 2f, middle.y + sizeY / 2f, 0f);
        BL = new Vector3(middle.x - sizeX / 2f, middle.y - sizeY / 2f, 0f);
        BR = new Vector3(middle.x + sizeX / 2f, middle.y - sizeY / 2f, 0f);

        //From screen to world
        RaycastHit hit;
        int i = 0;
        //Fire ray from camera
        if (Physics.Raycast(Camera.main.ScreenPointToRay(TL), out hit, 200f, 1 << 8))
        {
            TL = hit.point;
            i++;
        }
        if (Physics.Raycast(Camera.main.ScreenPointToRay(TR), out hit, 200f, 1 << 8))
        {
            TR = hit.point;
            i++;
        }
        if (Physics.Raycast(Camera.main.ScreenPointToRay(BL), out hit, 200f, 1 << 8))
        {
            BL = hit.point;
            i++;
        }
        if (Physics.Raycast(Camera.main.ScreenPointToRay(BR), out hit, 200f, 1 << 8))
        {
            BR = hit.point;
            i++;
        }

        //Could we create a square?
        hasCreatedSquare = false;

        //We could find 4 points
        if (i == 4)
        {
            //Display the corners for debug
            //sphere1.position = TL;
            //sphere2.position = TR;
            //sphere3.position = BL;
            //sphere4.position = BR;

            hasCreatedSquare = true;
        }
    }
}
