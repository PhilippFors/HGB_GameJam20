using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maximumHealth;
    public float currentHealth;
    public bool invulnverable = false;
    public float Iframes;

    private void Start()
    {
        currentHealth = maximumHealth;
    }

    public void ResetHealth()
    {
        currentHealth = maximumHealth;
    }
    public void Damage()
    {
        if (!invulnverable)
            StartCoroutine(Wait());
        else
            return;

        if (currentHealth > 0)
            currentHealth--;

        if (currentHealth == 0)
        {
            GetComponent<PlayerController>().alive = false;
            RespawnSystem.instance.StartReset();
        }
    }

    IEnumerator Wait()
    {
        invulnverable = true;
        yield return new WaitForSeconds(Iframes);
        invulnverable = false;
    }
}
