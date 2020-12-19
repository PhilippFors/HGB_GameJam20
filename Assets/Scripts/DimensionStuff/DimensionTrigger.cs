using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionTrigger : MonoBehaviour
{
    public List<Collectable> collectables = new List<Collectable>();
    List<Collectable> catchQueue = new List<Collectable>();
    [SerializeField] BoxCollider col;
    public Selectionbox selectionBox;
    private void OnTriggerEnter(Collider other)
    {
        Collectable h = other.GetComponent<Collectable>();

        if (h != null && !h.released)
        {
            h.releasable = false;
            if (h.transform.position.x >= transform.position.x)
            {
                h.fromRight = true;
                h.fromLeft = false;
            }
            else
            {
                h.fromRight = false;
                h.fromLeft = true;
            }
            Bullet bullet = h.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.rb.velocity = Vector3.zero;
                bullet.rb.angularVelocity = Vector3.zero;
            }

            if (selectionBox.selecting)
            {
                catchQueue.Add(h);
            }
            else
            {
                h.releasable = true;
                collectables.Add(h);
            }

            h.gameObject.SetActive(false);
        }
    }

    public void Release()
    {
        if (collectables.Count == 0)
            return;

        foreach (Collectable coll in collectables)
            if (coll.releasable)
            {
                coll.released = true;

                Bullet bullet = coll.gameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    if (coll.fromLeft)
                    {
                        coll.transform.position = transform.position - new Vector3(col.size.x / 2, 0, 0);
                        bullet.InitBullet(-bullet.forward, 2, 0);
                    }
                    if (coll.fromRight)
                    {
                        coll.transform.position = transform.position + new Vector3(col.size.x / 2, 0, 0);
                        bullet.InitBullet(bullet.forward, 2, 0);
                    }
                }

                collectables.Remove(coll);
                return;
            }
    }

    public void QueueWorker()
    {
        foreach (Collectable coll in catchQueue)
        {
            coll.releasable = true;
            collectables.Add(coll);
        }

        catchQueue = new List<Collectable>();
    }
}
