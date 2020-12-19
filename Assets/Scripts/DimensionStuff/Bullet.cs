using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Bullet : Collectable
{
    public float speed;
    public float damage;
    public Rigidbody rb => GetComponent<Rigidbody>();
    public Vector3 forward;
    public Coroutine coroutine;
    private void Start()
    {
        forward = Camera.main.transform.right;
        // InitBullet(forward, 2, 0);
    }
    public void InitBullet(Vector3 dir, float force, float dmg)
    {
        damage = dmg;
        speed = force;
        rb.AddForce(dir * force, ForceMode.Impulse);
        // coroutine = StartCoroutine(Wait());
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth p = other.GetComponent<PlayerHealth>();
        if (p != null)
        {
            p.Damage();
            Destroy(gameObject);
        }
        
        HideEnemy e = other.GetComponent<HideEnemy>();
        if (e != null)
        {
            Destroy(gameObject);
            e.Damage();
        }

        if (other.gameObject.tag.Equals("Walls"))
            Destroy(gameObject);
    }

    public void StopWait()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
        if (!releasable || released)
            Destroy(gameObject);
    }
}
