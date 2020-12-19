using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maximumHealth;

    public float currentHealth;

    private void Start()
    {
        currentHealth = maximumHealth;
    }

    public void Damage()
    {
        if (currentHealth > 0)
            currentHealth--;

        if (currentHealth == 0)
        {
            Debug.Log("You are dead");
        }
    }
}
