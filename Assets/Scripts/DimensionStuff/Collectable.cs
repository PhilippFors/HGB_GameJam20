using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public bool fromLeft;
    public bool fromRight;

    public bool released = false;
    public bool releasable = false;
}
