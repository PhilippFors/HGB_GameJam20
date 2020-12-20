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
        
            Bullet bullet = h.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.GetComponent<SphereCollider>().enabled = false;
                bullet.StopWait();
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
    
    public void ReleaseToRight()
    {
        if (collectables.Count == 0)
            return;

        foreach (Collectable coll in collectables)
            if (coll.releasable)
            {
                coll.released = true;
                coll.transform.position = transform.position;
                Bullet bullet = coll.gameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.GetComponent<SphereCollider>().enabled = true;
                    bullet.rb.velocity = Vector3.zero;

                    bullet.gameObject.SetActive(true);

                    coll.transform.rotation = Quaternion.LookRotation(-bullet.forward);
                    bullet.InitBullet(-bullet.forward, bullet.speed, 0);
                }

                collectables.Remove(coll);
                return;
            }
    }

    public void ReleaserToLeft()
    {
        if (collectables.Count == 0)
            return;

        foreach (Collectable coll in collectables)
            if (coll.releasable)
            {
                coll.released = true;
                coll.transform.position = transform.position;
                Bullet bullet = coll.gameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.GetComponent<SphereCollider>().enabled = true;
                    bullet.rb.velocity = Vector3.zero;

                    bullet.gameObject.SetActive(true);

                    coll.transform.rotation = Quaternion.LookRotation(bullet.forward);
                    bullet.InitBullet(bullet.forward, bullet.speed, 0);
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
