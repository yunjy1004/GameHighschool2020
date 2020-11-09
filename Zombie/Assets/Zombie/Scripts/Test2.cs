using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour, IDamageable
{

    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        //Destroy(gameObject);
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;

        rigidbody.AddForce(-hitNormal * 500);
    }
}
