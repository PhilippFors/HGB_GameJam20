using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClone : MonoBehaviour
{
    public PlayerController original;
    public float zOffset;

    void LateUpdate()
    {
        transform.position = original.transform.position + new Vector3(0, 0, zOffset);
        transform.rotation = original.transform.rotation;
    }
}
