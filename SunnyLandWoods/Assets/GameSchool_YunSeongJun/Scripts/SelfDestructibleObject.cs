using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunSeongJun
{
    public class SelfDestructibleObject : MonoBehaviour
    {

        public void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}
