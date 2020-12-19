using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    PlayerController playerController;
    public float cameraFollowSpeed = 5f;
    public float yOffset;
    Vector3 s;

    private void Start()
    {
        playerController = player.gameObject.GetComponent<PlayerController>();
        StartCoroutine(WaitForStart());
    }

    private void Update()
    {
        s = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, s, Time.deltaTime * cameraFollowSpeed);
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForEndOfFrame();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, transform.position.z);
    }
}
