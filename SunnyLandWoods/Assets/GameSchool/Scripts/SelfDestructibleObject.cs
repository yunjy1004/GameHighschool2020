using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructibleObject : MonoBehaviour
{

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
