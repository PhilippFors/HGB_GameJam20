using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClone : MonoBehaviour
{
    public PlayerController original;
    public float zOffset;
    // Update is called once per frame
    private void Start()
    {
        // yOffset = Vector3.Distance(transform.position, original.transform.position);
    }
    void LateUpdate()
    {
        transform.position = original.transform.position + new Vector3(0, 0, zOffset);
        transform.rotation = original.transform.rotation;
    }
}
