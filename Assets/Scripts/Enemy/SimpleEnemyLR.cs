using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyLR : HideEnemy
{
    public float speed;
    public float rayCastDownlength;
    public float rayCastForwardlength;
    Vector3 forward;
    public LayerMask floor;
    public bool moveR;

    bool noFloor;
    public Transform[] rays;

    public BoxCollider boxCol;
    public BoxCollider weapon;

    private void Start()
    {
        forward = Camera.main.transform.right;
    }

    private void Update()
    {
        DoRayCast();
        if (isDisabled)
            return;

        CheckForEdge();
        CheckForWall();

        if (!noFloor)
            Move();
        else
            GetComponent<Rigidbody>().useGravity = true;
    }

    void Move()
    {

        if (moveR)
        {
            transform.position += forward * speed * Time.deltaTime;
            // transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        }
        else
        {
            transform.position += -forward * speed * Time.deltaTime;
            // transform.rotation = Quaternion.LookRotation(-forward, Vector3.up);
        }
    }

    void CheckForEdge()
    {
        int i = 0;
        foreach (Transform rayem in rays)
            if (!Physics.Raycast(rayem.position, Vector3.down, rayCastDownlength, floor, QueryTriggerInteraction.Ignore))
            {
                if (moveR)
                    moveR = false;
                else
                    moveR = true;

                i++;
            }

        if (i == 2)
            noFloor = true;
        else
            noFloor = false;
    }

    void CheckForWall()
    {
        foreach (Transform rayem in rays)
            if (Physics.Raycast(rayem.position, rayem.forward, rayCastForwardlength, floor, QueryTriggerInteraction.Ignore))
            {
                if (moveR)
                    moveR = false;
                else
                    moveR = true;
                return;
            }
    }

    public override void Disable()
    {
        // GetComponent<Rigidbody>().useGravity = false;
        isDisabled = true;
        // boxCol.enabled = false;
        weapon.enabled = false;
    }

    public override void Enable()
    {
        isDisabled = false;
        // boxCol.enabled = true;
        weapon.enabled = true;
        // GetComponent<Rigidbody>().useGravity = true;
    }
}
