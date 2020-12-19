using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public Transform target;
    public LayerMask grappleMask;
    public InputManager input;
    public float grappleDist;
    RaycastHit hit;
    public bool canGrapple;

    private void Update()
    {
        FindGrapplePoint();
    }
    void FindGrapplePoint()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(input.mousePos);
        if (Physics.Raycast(cameraRay, out hit, 20f, grappleMask))
        {
            // squareStartPos = cameraRay.GetPoint(rayLength);
            GrapplePoint p = hit.transform.GetComponent<GrapplePoint>();
            if (p != null)
                target = p.transform;

            canGrapple = Vector3.Distance(transform.position, target.position) <= grappleDist;
        }
        // else
        // {
        //     target = null;
        // }
    }

    // void GrappleTo()
    // {
    //     Debug.Log("grapple");
    //     if (target != null)
    //     {
    //         vel = Vector3.Scale(transform.forward,
    //                                    dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * drag + 1)) / -Time.deltaTime),
    //                                                               0,
    //                                                               (Mathf.Log(1f / (Time.deltaTime * drag + 1)) / -Time.deltaTime)));
    //     }

    //     vel.x /= 1 + drag * Time.deltaTime;
    // }
}
