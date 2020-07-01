using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float m_speed = 25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rigidbody = /*gameObject*/GetComponent<Rigidbody>();

        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        rigidbody.AddForce(new Vector3(xAxis, 0, yAxis) * m_speed);

        float fireAxis = Input.GetAxis("Fire1");

        if (fireAxis > 0.95f)
            Die();

        //if (Input.GetAxis("Horizontal"))
        //{
        //    //transform.position += Vector3.left * m_speed * Time.deltaTime;
        //    rigidbody.AddForce(Vector3.left * m_speed);
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    rigidbody.AddForce(Vector3.right * m_speed);
        //}
        //else if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    rigidbody.AddForce(Vector3.forward * m_speed);
        //}
        //else if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    rigidbody.AddForce(Vector3.back * m_speed);
        //}
    }

    public void Die()
    {
        Debug.Log("사망");
        gameObject.SetActive(false);
    }
}
