using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //m_Velocity = transform.forward;
    }
    public Vector3 m_Velocity;

    public float m_Speed = 5f;

    public float m_DestoryCooltime = 5f;


    // Update is called once per frame
    void Update()
    {
        Rigidbody rigidbody = /*gameObject.*/GetComponent<Rigidbody>();

        rigidbody.velocity = m_Velocity * m_Speed;

        m_DestoryCooltime -= Time.deltaTime;

        if (m_DestoryCooltime <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.tag == "Player")
        {
            var player = other.attachedRigidbody.GetComponent<PlayerController>();
            player.Die();
        }
    }
}
