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
            GrapplePoint p = hit.transform.GetComponent<GrapplePoint>();
            if (p != null)
            {
                if (!Physics.Raycast(transform.position, p.transform.position - transform.position, 30f, LayerMask.GetMask("Walls")))
                {
                    canGrapple = Vector3.Distance(transform.position, p.transform.position) <= grappleDist;
                    if (canGrapple)
                    {
                        target = p.transform;
                        p.SetActive();
                    }
                }
            }
        }
        else
        {
            if (target != null)
                target.GetComponent<GrapplePoint>().SetInactive();

            target = null;
        }
    }
}
