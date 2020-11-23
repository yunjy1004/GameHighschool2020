using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : MonoBehaviour
{
    public Rigidbody m_Rigidbody;
    public float m_Speed = 1f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var xAxis = Input.GetAxis("Horizontal");
        var zAxis = Input.GetAxis("Vertical");

        //입력이 없을경우
        if(xAxis == 0 && zAxis == 0)
        {
            var velocity = m_Rigidbody.velocity;

            float breakFactor = 0.8f;

            velocity.x = velocity.x * (1f - breakFactor * Time.deltaTime);
            velocity.z = velocity.z * (1f - breakFactor * Time.deltaTime);

            m_Rigidbody.velocity = velocity;

        }
        //입력이 있을경우
        else
        {
            var velocity = new Vector3(xAxis, 0, zAxis);
            velocity = velocity.normalized;
            velocity *= m_Speed;
            velocity.y = m_Rigidbody.velocity.y;

            m_Rigidbody.velocity = velocity;
        }
    }

}
