using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HideEnemy : MonoBehaviour
{
    public bool exists;
    public bool showBoth;
    public GameObject original;
    public GameObject copy;
    public MeshRenderer[] rend;
    public MeshRenderer[] rendCopy;

    [HideInInspector] public BoxCollider boxCol;
    public bool isDisabled;
    public Transform[] rayEmitters;
    public LayerMask dimension;
    int i = 0;
    public abstract void Disable();

    public abstract void Enable();

    public virtual void INIT()
    {
        if (copy != null)
        {
            boxCol = GetComponent<BoxCollider>();
            if (exists)
            {
                GetComponentInChildren<Animator>().enabled = true;


                if (boxCol != null)
                    boxCol.enabled = true;
                foreach (MeshRenderer r in rend)
                    r.enabled = true;

                if (showBoth)
                {
                    copy.GetComponentInChildren<Animator>().enabled = true;
                    foreach (MeshRenderer rr in rendCopy)
                        rr.enabled = true;
                }
                else
                {
                    copy.GetComponentInChildren<Animator>().enabled = false;
                    foreach (MeshRenderer rr in rendCopy)
                        rr.enabled = false;

                }

            }
            else
            {
                GetComponentInChildren<Animator>().enabled = false;
                copy.GetComponentInChildren<Animator>().enabled = true;
                if (boxCol != null)
                    boxCol.enabled = false;
                foreach (MeshRenderer r in rend)
                    r.enabled = false;

                foreach (MeshRenderer rr in rendCopy)
                    rr.enabled = true;
            }
        }
        else
        {
            exists = true;
        }
    }
    public void UpdateCopy()
    {
        if (copy != null)
            copy.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + GameSettings.instance.zOffset);
    }
    public void DoRayCast()
    {
        RaycastHit hit;
        i = 0;
        foreach (Transform raye in rayEmitters)
        {
            Ray ray = new Ray(raye.position, Camera.main.transform.position - raye.position);
            if (Physics.Raycast(ray, out hit, 20f, dimension))
            {
                if (hit.transform.GetComponent<DimensionVision>())
                {
                    i++;
                }
            }
        }
        if (exists)
        {
            if (i == 2)
            {
                if (showBoth)
                    Enable();
                else
                    Disable();
            }
            else
                Enable();
        }
        else
        {
            if (i == 2)
                Enable();
            else
                Disable();
        }
    }

    public void Damage()
    {
        Destroy(gameObject);
    }
}
