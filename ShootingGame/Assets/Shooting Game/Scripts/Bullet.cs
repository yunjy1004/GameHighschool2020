﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float m_Speed = 2f;

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = transform.up * m_Speed;
        Vector3 movement = velocity * Time.deltaTime;
        transform.position += movement;
    }
}
