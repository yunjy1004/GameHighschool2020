using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float m_speed = 25f;

    public float m_DestroyCooltime = 5f;
    // Update is called once per frame
    void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.AddForce(transform.forward * m_speed);

        m_DestroyCooltime -= Time.deltaTime;

        if (m_DestroyCooltime <= 0)
            gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.tag == "Player" )
        {
            var Player = other.attachedRigidbody.GetComponent<PlayerController>();
            Player.Die();

        }
    }

}
