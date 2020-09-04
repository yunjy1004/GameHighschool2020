using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    public Transform m_Sprite;

    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;

    public float m_ClimbSpeed = 3f;
    public float m_XAxisSpeed = 3f;
    public float m_YJumpPower = 3f;
    public float m_Speed = 50f;

    public int m_JumpCount = 0;

    public bool m_IsJumping = false;
    public bool m_IsTouchLadder = false;
    public bool m_IsClimbing = false;

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

        if (m_IsTouchLadder && Mathf.Abs(yAxis) > 0.5f)
        {
            m_IsClimbing = true;
        }

        if (!m_IsClimbing)
        {

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
         else
        {

            m_Rigidbody2D.constraints = m_Rigidbody2D.constraints | RigidbodyConstraints2D.FreezePosition;

            Vector3 movement = Vector3.zero;
            movement.x = xAxis * m_ClimbSpeed * Time.deltaTime;
            movement.y = yAxis * m_ClimbSpeed * Time.deltaTime;

            transform.position += movement;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ClimbingExit();
            }
        }

        //Update 사다리타기 점프 캔슬하는데
        m_Animator.SetBool("IsClimbing", m_IsClimbing);
        m_Animator.SetFloat("ClimbSpeed", Mathf.Abs(xAxis) + Mathf.Abs(yAxis));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
            if (contact.normal.y > 0.5f)
            {
                m_JumpCount = 0;
            }
        }

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                m_JumpCount = 0;

                if (contact.rigidbody)
                {
                    var hp = contact.rigidbody.GetComponent<HPComponent>();
                    if (hp)
                    {
                        Destroy(hp.gameObject);
                    }
                }
            }
        }
        /*if (collision.gameObject.tag == "Ground")
        {
            m_JumpCount = 0;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ladder")
        {
            m_IsTouchLadder = true;
        }
        else if(collision.tag == "Item")
        {
            var item = collision.GetComponent<ItemComponent>();
            if(item != null)
                item.PickUpItem();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Ladder")
        {
            m_IsTouchLadder = false;
            ClimbingExit();
        }
    }

    private void ClimbingExit()
    {
        m_Rigidbody2D.constraints = m_Rigidbody2D.constraints & ~RigidbodyConstraints2D.FreezePosition;
        m_IsClimbing = false;

        m_Animator.SetBool("IsClimbing", m_IsClimbing);
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
