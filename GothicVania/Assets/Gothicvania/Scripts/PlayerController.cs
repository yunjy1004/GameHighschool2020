using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public enum State
    {
        None,
        GroundWalk,
        Jump,
        Attack,
        Hurt
    }

    public State m_State = State.None;

    public float m_CurrentSpeed = 1f;
    public float m_CurrentJumpSpeed = 3f;

    public SpriteRenderer m_Sprite;
    public Rigidbody2D m_Rigidbody;

    protected void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (m_State)
        {
            case State.None:
                OnTick_None();
                break;
            case State.GroundWalk:
                OnTick_GroundWalk();
                break;
            case State.Jump:
                OnTick_Jump();
                break;
            default:
                break;
        }
    }

    public void OnTick_None()
    {
        if (m_IsGround)
        {
            m_State = State.GroundWalk;
        }
    }

    public void OnTick_Jump()
    {
        float xAxis = Input.GetAxis("Horizontal");

        float xMovement = m_CurrentSpeed * xAxis * Time.deltaTime;
        Vector3 movement = Vector3.right * xMovement;
        transform.position += movement;

        if (xAxis < 0)
        {
            m_Sprite.flipX = true;
        }
        else if (xAxis > 0)
        {
            m_Sprite.flipX = false;
        }

        if (m_IsGround)
            m_State = State.GroundWalk;
    }

    public float m_JumpSpeed = 0;
    public float m_Gravity = 5f;

    public void OnTick_GroundWalk()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        bool inputJump = Input.GetKeyDown(KeyCode.C);

        if (inputJump)
        {
            m_State = State.Jump;
        }

        if (inputJump)
        {
            m_JumpSpeed = m_CurrentJumpSpeed;
        }

        if (m_IsGround)
        {
            m_JumpSpeed = 0;
        }
        else
        {
            m_JumpSpeed -= m_Gravity * Time.deltaTime;

            if (m_JumpSpeed <= 10)
                m_JumpSpeed = 10;
        }

        float xMovement = m_CurrentSpeed * xAxis * Time.deltaTime;
        Vector3 movement = Vector3.right * xMovement;
        transform.position += movement;

        //구버젼
        //if (yAxis > 0)
        //{
        //    m_State = State.Jump;
        //}

        //if (yAxis > 0)
        //{
        //    Vector2 velocity = m_Rigidbody.velocity;
        //    velocity.y = m_CurrentJumpSpeed;
        //    m_Rigidbody.velocity = velocity;
        //}

        //if (xAxis < 0)
        //{
        //    m_Sprite.flipX = true;
        //}
        //else if (xAxis > 0)
        //{
        //    m_Sprite.flipX = false;
        //}

        //트랜스폼으로 이산적으로 움직임
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //foreach(ContactPoint2D contact 
        //    in collision.contacts)
        //{
        //    if(contact.collider.tag =="Ground" 
        //    && contact.otherCollider.tag == "WalkCollider")
        //    {
        //        if(contact.normal.y > 0.8f)
        //        {
        //            Debug.Log("캐릭터 바닥에 있음");

        //            m_IsGround = true;
        //        }
        //    }
        //}
    }

    public bool m_IsGround = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact
            in collision.contacts)
        {
            if (contact.collider.tag == "Ground"
            && contact.otherCollider.tag == "WalkCollider")
            {
                if (contact.normal.y > 0.8f)
                {
                    Debug.Log("캐릭터 바닥에 있음");

                    m_IsGround = true;
                }
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground"
        && collision.otherCollider.tag == "WalkCollider")
        {
            m_IsGround = false;
        }
    }
}