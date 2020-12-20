using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : HideEnemy
{
    public float fireRate;
    public float lerpSpeed;
    float time = 0;
    public Transform bulletEmitter;
    [HideInInspector] public Transform player;
    public GameObject bullet;
    public Transform barrels;
    public Animator anim;
    public bool faceLeft;
    bool canShoot = false;
    Quaternion ogrot;

    public BoxCollider boxCollider;
    private void Start()
    {
        INIT();
        ogrot = barrels.transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void INIT()
    {
        base.INIT();
        if (exists)
            Enable();
        else
            Disable();
    }
    private void Update()
    {
        UpdateCopy();
        DoRayCast();
        if (exists && isDisabled)
            return;

        LookAtPlayer();
        Shoot();

    }

    public void Shoot()
    {
        if (time >= fireRate)
        {
            canShoot = true;
        }
        time += Time.deltaTime;
        if (IsPlayerInFront())
            if (canShoot)
            {
                time -= fireRate;
                anim.Play("Turret_Shoot");
                Bullet b = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.rotation).GetComponent<Bullet>();
                b.InitBullet(bulletEmitter.forward, 4, 5);
                canShoot = false;
            }
    }
    public void LookAtPlayer()
    {
        if (IsPlayerInFront())
        {
            // barrels.transform.rotation = Quaternion.Lerp(barrels.transform.rotation, Quaternion.LookRotation(player.transform.position - barrels.transform.position), Time.deltaTime * lerpSpeed);
        }
        else
        {
            // barrels.transform.rotation = Quaternion.Lerp(barrels.transform.rotation, ogrot, Time.deltaTime * lerpSpeed);
        }
    }

    public bool IsPlayerInFront()
    {
        if (faceLeft)
        {
            return transform.position.x - 1f > player.position.x;
        }
        else
        {
            return transform.position.x + 1f < player.position.x;
        }
    }

    public override void Disable()
    {
        isDisabled = true;
        boxCollider.enabled = false;
        foreach (MeshRenderer r in rend)
            r.enabled = false;
    }

    public override void Enable()
    {
        isDisabled = false;
        boxCollider.enabled = true;
        foreach (MeshRenderer r in rend)
            r.enabled = true;
    }
}
