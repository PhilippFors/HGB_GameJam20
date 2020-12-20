using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public float zOffset;
    public static GameSettings instance;
    void Awake()
    {
        instance = this;
    }

}
