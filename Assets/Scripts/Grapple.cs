using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public GrapplePoint target;
    public LayerMask grappleMask;
    public InputManager input;
    public float grappleDist;
    RaycastHit hit;
    public bool canGrapple;
    Ray cameraRay;
    private void Update()
    {
        FindGrapplePoint();
    }
    void FindGrapplePoint()
    {
        cameraRay = Camera.main.ScreenPointToRay(input.mousePos);

        if (Physics.Raycast(cameraRay, out hit, 30f, grappleMask))
        {
            GrapplePoint p = hit.transform.GetComponent<GrapplePoint>();
            if (p != null)
            {
                target = p;
                if (!Physics.Raycast(transform.position, target.transform.position - transform.position, grappleDist + 1f, LayerMask.GetMask("Walls")))
                {
                    canGrapple = Vector3.Distance(transform.position, target.transform.position) <= grappleDist;
                    target.SetActive();
                }
                else
                {
                    target.SetInactive();

                    target = null;
                }
            }
        }
        else
        {
            if (target != null)
                target.SetInactive();

            target = null;
        }
        // if (target != null)
        //     target.GetComponent<GrapplePoint>().SetActive();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(cameraRay);
    }
}
