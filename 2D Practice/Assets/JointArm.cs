using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointArm : MonoBehaviour
{

    public Transform m_Target;
    public Vector3 m_Offset;
    // Start is called before the first frame update
    void Start()
    {
        m_Offset = transform.position - m_Target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_Target.transform.position + m_Offset;
    }
}
