using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyLR : MonoBehaviour
{
    public float speed;
    public float rayCastDownlength;
    public float rayCastForwardlength;
    Vector3 forward;
    public LayerMask floor;
    public bool moveR;

    bool noFloor;
    public Transform[] rayEmitter;
    private void Start()
    {
        forward = Camera.main.transform.right;
    }

    private void Update()
    {
        if (!noFloor)
            Move();
    }

    void Move()
    {
        CheckForEdge();
        CheckForWall();
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
        foreach (Transform rayem in rayEmitter)
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
    }

    void CheckForWall()
    {
        foreach (Transform rayem in rayEmitter)
            if (Physics.Raycast(rayem.position, rayem.forward, rayCastForwardlength, floor, QueryTriggerInteraction.Ignore))
            {
                if (moveR)
                    moveR = false;
                else
                    moveR = true;
                return;
            }

    }
}
