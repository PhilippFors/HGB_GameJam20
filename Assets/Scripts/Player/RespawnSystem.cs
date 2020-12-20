using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] InputManager input;
    public Transform activeCheckpoint;
    public static RespawnSystem instance;

    private void Awake()
    {
        instance = this;
    }
    public void StartReset()
    {
        StartCoroutine(DoReset());
    }

    IEnumerator DoReset()
    {
        //Transition
        input.inputControls.Disable();
        yield return new WaitForSeconds(1f);
        ToCheckpoint();

        //Transition
        yield return new WaitForSeconds(1f);
        player.TurnOn();
        player.GetComponent<PlayerHealth>().ResetHealth();
        input.inputControls.Enable();
    }

    void ToCheckpoint()
    {
        player.transform.position = activeCheckpoint.position;
    }
}
