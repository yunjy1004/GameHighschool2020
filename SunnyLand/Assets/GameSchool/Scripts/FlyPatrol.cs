﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPatrol : MonoBehaviour
{
    private Vector3 m_StartPps;
    public Vector3 m_PatrolPos;
    public float m_PatrolTerm = 2f;

    private float m_PatrolTime;
    public bool m_Go = true;

    public void Start()
    {
        m_StartPps = transform.position;
    }

    void Update()
    {
        m_PatrolTime += Time.deltaTime;

        var lap = m_PatrolTime / m_PatrolTerm;
        var mod = m_PatrolTime % m_PatrolTerm;

        if (lap % 2 < 1)
        {
            if (m_StartPps.x <= m_PatrolPos.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
            transform.position = Vector3.Lerp(m_StartPps, m_PatrolPos, mod);
        }
        else
        {
            if (m_StartPps.x >= m_PatrolPos.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }

            transform.position = Vector3.Lerp(m_PatrolPos, m_StartPps, mod);
        }
    }
}
