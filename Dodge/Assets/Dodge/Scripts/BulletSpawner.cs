using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject m_Bullet;

    //public Transform m_PlayerTransform;

    public float m_RotationSpeed = 60f;
    public float m_AttackInterval = 1f;
    public float m_AttackCooltime = 0f;

    // Update is called once per frame
    void Update()
    {
        m_AttackCooltime += Time.deltaTime;
        if(m_AttackCooltime >= m_AttackInterval)
        {
            GameObject bullet = GameObject.Instantiate(m_Bullet);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            m_AttackCooltime = 0;
        }

        //GameObject.Find("게임 오브젝트의 이름"); //게임오브젝트의 이름
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //player
        //GameObject.FindObjectOfType<PlayerController>();

        //GameObject.FindGameObjectsWithTag("Player"); //모든 player
        //GameObject.FindObjectOfType<PlayerController>(); //모든 player controller


        if (player != null)
        {
            Vector3 attacketPoint = player.transform.position;
            attacketPoint.y = transform.position.y;
            transform.LookAt(attacketPoint);
        }
        //transform.Rotate(0, m_RotationSpeed * Time.deltaTime, 0);
    }
}
