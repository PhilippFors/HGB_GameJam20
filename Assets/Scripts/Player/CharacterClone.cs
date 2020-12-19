using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClone : MonoBehaviour
{
    public PlayerController original;
    public float xOffset;
    // Update is called once per frame
    private void Start()
    {
        xOffset = Vector3.Distance(transform.position, original.transform.position);
    }
    void LateUpdate()
    {
        transform.position = original.transform.position + new Vector3(xOffset, 0, 0);
        transform.rotation = original.transform.rotation;
    }
}
