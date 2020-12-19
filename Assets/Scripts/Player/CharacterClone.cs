using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClone : MonoBehaviour
{
    public PlayerController controller;
    public float xOffset;
    // Update is called once per frame
    private void Start()
    {
        xOffset = Vector3.Distance(transform.position, controller.transform.position);
    }
    void LateUpdate()
    {
        transform.position = controller.transform.position + new Vector3(xOffset, 0, 0);
        transform.rotation = controller.transform.rotation;
    }
}
