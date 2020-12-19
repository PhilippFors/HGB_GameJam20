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
    private void Start()
    {
        forward = Camera.main.transform.right;
        InitBullet(forward, 2, 0);
    }
    public void InitBullet(Vector3 dir, float force, float dmg)
    {
        damage = dmg;
        rb.AddForce(dir * force, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController p = other.GetComponent<PlayerController>();
        if (p != null)
        {
            Debug.Log("Damage");
            Destroy(this.gameObject);
        }
    }
}
