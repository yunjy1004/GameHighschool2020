using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmall : MonoBehaviour
{
    public float m_Speed = 3f;

    public float m_LifeTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = transform.up * m_Speed;
        Vector3 movement = velocity * Time.deltaTime;
        transform.position -= movement;

        if (m_LifeTime <= 0)
            Destroy(gameObject);

        m_LifeTime -= Time.deltaTime;
        
    }
}
