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

    public BoxCollider weapon;

    private void Start()
    {
        INIT();
        forward = Camera.main.transform.right;
    }
    public override void INIT()
    {
        base.INIT();
        if (exists)
            Enable();
        else
            Disable();

        boxCol.enabled = false;
    }
    private void Update()
    {
        UpdateCopy();
        DoRayCast();
        if (exists && isDisabled)
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
            }
    }

    public override void Disable()
    {
        GetComponentInChildren<Animator>().enabled = false;
        isDisabled = true;
        // boxCol.enabled = false;
        weapon.enabled = false;
        foreach (MeshRenderer r in rend)
            r.enabled = false;
    }

    public override void Enable()
    {
        GetComponentInChildren<Animator>().enabled = true;
        isDisabled = false;
        weapon.enabled = true;
        foreach (MeshRenderer r in rend)
            r.enabled = true;
    }
}
