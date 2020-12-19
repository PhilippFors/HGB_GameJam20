using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float fireRate;
    float time = 0;
    public Transform player;
    RaycastHit hit;

    public Transform barrels;
    public Animator anim;
    public bool faceLeft;
    bool canShoot = false;
    private void Start()
    {

    }
    private void Update()
    {
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
                //Shoot particle
                canShoot = false;
            }
    }
    public void LookAtPlayer()
    {
        if (IsPlayerInFront())
        {
            barrels.transform.LookAt(player);
        }
    }

    public bool IsPlayerInFront()
    {
        if (faceLeft)
        {
            return transform.position.x + 1f > player.position.x;
        }
        else
        {
            return transform.position.x - 1f < player.position.x;
        }
    }
}
