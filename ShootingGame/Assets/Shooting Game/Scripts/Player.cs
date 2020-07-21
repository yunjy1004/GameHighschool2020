using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_Speed = 1f;

    public Bullet m_BulletPrefab;
    public float m_AttackDelay = 0.5f;
    private float m_AttackCooldown = 0f;

    public Transform[] m_FireMuzzles; //총구 위치
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Translate(Vector3.left * m_Speed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Translate(Vector3.right * m_Speed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.Translate(Vector3.up * m_Speed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.Translate(Vector3.down * m_Speed * Time.deltaTime);
        //}

        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector2 inputValue = new Vector2(xAxis, yAxis).normalized;

        Vector3 movement = inputValue * m_Speed * Time.deltaTime;
        transform.position += movement;

        //총알 발사
        //GameObject.Instantiate
        if(Input.GetKey(KeyCode.Space)&& m_AttackCooldown <= 0)
        {
            //총알 생성
            foreach (var fireMuzzle in m_FireMuzzles)
            {
                var bullet = GameObject.Instantiate(m_BulletPrefab, fireMuzzle.position, fireMuzzle.rotation);
            }
            m_AttackCooldown = m_AttackDelay;
            
        }
        m_AttackCooldown -= Time.deltaTime;
    }
}
