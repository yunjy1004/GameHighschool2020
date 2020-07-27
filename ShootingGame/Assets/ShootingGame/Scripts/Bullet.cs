using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float m_Speed = 2f;
    public float m_BulletTime = 3f;

    private void Update()
    {
        Vector3 velocity = transform.up * m_Speed;
        Vector3 movement = velocity * Time.deltaTime;
        transform.position += movement;

    }

    public void Die()
    {
        if(m_BulletTime == 0)
        { 
            Destroy(gameObject);
        }
        m_BulletTime -= Time.deltaTime;
    }
}
