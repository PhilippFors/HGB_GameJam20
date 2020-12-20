using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maximumHealth;
    public int currentHealth;
    public bool invulnverable = false;
    public float Iseconds;
    public PlayerHealthUI healthUI;
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
            GetComponent<PlayerController>().TurnOff();
            RespawnSystem.instance.StartReset();
        }
        healthUI.UpdateUI(currentHealth);
    }

    IEnumerator Wait()
    {
        invulnverable = true;
        yield return new WaitForSeconds(Iseconds);
        invulnverable = false;
    }
}
