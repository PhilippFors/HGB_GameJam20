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
    bool faceRight;
    bool canShoot = true;
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
        if (time <= fireRate)
        {
            time += Time.deltaTime;
            canShoot = true;
        }

        if (canShoot)
        {
            time -= fireRate;
            //anime.Play("Shoot")
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
        if (faceRight)
        {
            return transform.position.x + 1f > player.position.x;
        }
        else
        {
            return transform.position.x - 1f < player.position.x;
        }
    }
}
