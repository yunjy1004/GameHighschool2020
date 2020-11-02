using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunSeongJun
{
    public class FollowBackground : MonoBehaviour
    {
        public float m_Grid = 20f;

        public GameObject[] m_Background;
        public Transform m_Target;

        public int m_Oldmod = 0;

        public void Update()
        {
            int mod = Mathf.RoundToInt(m_Target.position.x / m_Grid); //버림;

            //mathf.Floor(); 올림
            //mathf.Ceil(); 내림
            //mathf.ClosestPowerOfTwo(); 반올림

            //grid를 넘어서는 이동이 발생됨
            //갱신이 필요
            if (m_Oldmod != mod)
            {
                foreach (var background in m_Background)
                {
                    var pos = background.transform.position;
                    pos.x += m_Grid * (mod - m_Oldmod);
                    background.transform.position = pos;
                }
            }

            foreach (var background in m_Background)
            {
                var pos = background.transform.position;
                pos.y = m_Target.position.y;
                background.transform.position = pos;
            }

            m_Oldmod = mod;
        }
    }
}
