using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    public Transform m_Sprite;

    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;

    public float m_XAxisSpeed = 3f;
    public float m_YJumpPower = 3f;
    public float m_Speed = 50f;

    public int m_JumpCount = 0;

    public bool m_IsJumping = false;

    protected void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector2 velocity = m_Rigidbody2D.velocity;
        velocity.x = xAxis * m_XAxisSpeed;
        m_Rigidbody2D.velocity = velocity;

        if (xAxis > 0)
            m_Sprite.localScale = new Vector3(1, 1, 1);
        else if (xAxis < 0)
            m_Sprite.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKeyDown(KeyCode.UpArrow) && m_JumpCount <= 0)
        {
            m_Rigidbody2D.AddForce(Vector3.up * m_YJumpPower);
            m_JumpCount++;
        }

        //이거 추가
        m_IsJumping = Mathf.Abs(velocity.y) >= 0.5f ? true : false;

        //스크립트 2줄 작성
        m_Animator.SetBool("isJumping", m_IsJumping);
        m_Animator.SetFloat("Velocity X", Mathf.Abs(xAxis));

        //이거 추가
        m_Animator.SetFloat("Velocity Y", velocity.y);

        //var animator = GetComponent<Animator>();
        //animator.SetFloat("Velocity Y", velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
            if(contact.normal.y > 0.5f)
            {
                m_JumpCount = 0;
            }
        }
        /*if (collision.gameObject.tag == "Ground")
        {
            m_JumpCount = 0;
        }*/
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "Ladder")
    //    {
    //        if(Input.GetKeyDown(KeyCode.UpArrow))
    //        {
    //            m_Rigidbody2D.AddForce(Vector3.up * m_Speed);
    //        }
    //    }
    //}


}
