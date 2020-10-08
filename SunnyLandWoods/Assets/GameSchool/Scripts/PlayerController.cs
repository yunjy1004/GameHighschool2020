using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        None = 0,    //초기 상태
        Idle,
        Walking,
        Jumping
    }

    public State m_State = State.None;

    public void EnterState(State state)
    {
        switch (state)
        {
            case State.None:
                break;
            case State.Idle:
                {

                }
                break;
            case State.Walking:
                break;
            case State.Jumping:
                break;
            default:
                break;
        }
    }

    public void ExitState(State state)
    {
        switch (state)
        {
            case State.None:
                break;
            case State.Idle:
                {

                }
                break;
            case State.Walking:
                break;
            case State.Jumping:
                break;
            default:
                break;
        }
    }


    public void IntputHandlerProcess(State state)
    {
        switch (state)
        {
            case State.None:
                break;
            case State.Idle:
                {
                    m_xAxis = Input.GetAxis("Horizontal");
                    m_yAxis = Input.GetAxis("Vertical");
                }
                break;
            case State.Walking:
                {
                    m_xAxis = Input.GetAxis("Horizontal");
                    m_yAxis = Input.GetAxis("Vertical");
                }
                break;
            case State.Jumping:
                {
                    m_xAxis = Input.GetAxis("Horizontal");
                    m_yAxis = Input.GetAxis("Vertical");
                }
                break;
            default:
                break;
        }
    }


    public void FixedUpdateProcess(State state)
    {
        switch (state)
        {
            case State.None:
                break;
            case State.Idle:
                {
                    if (Mathf.Abs(m_xAxis) <= 0.1f)
                    {
                        Vector2 velocity = m_Rigidbody2D.velocity;
                        velocity.x = 0;
                        m_Rigidbody2D.velocity = velocity;
                    }
                    else
                    {
                        ChangeState(State.Walking);
                    }
                }
                break;
            case State.Walking:
                {
                    if (Mathf.Abs(m_xAxis) > 0.1f)
                    {
                        Vector2 velocity = m_Rigidbody2D.velocity;
                        velocity.x = m_xAxis * m_MovementSpeed;
                        m_Rigidbody2D.velocity = velocity;
                    }
                    else
                    {
                        ChangeState(State.Idle);
                    }
                }
                break;
            case State.Jumping:
                break;
            default:
                break;
        }
    }


    public void UpdateProcess(State state)
    {
        switch (state)
        {
            case State.None:
                break;
            case State.Idle:
                {

                }
                break;
            case State.Walking:
                break;
            case State.Jumping:
                break;
            default:
                break;
        }
    }


    public void ChangeState(State newState)
    {
        ExitState(m_State);

        m_State = newState;

        EnterState(m_State);
    }

    #region 유니티 생명주기

    public void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        ChangeState(State.Idle);
    }

    public void FixedUpdate()
    {
        FixedUpdateProcess(m_State);
    }


    public void Update()
    {
        IntputHandlerProcess(m_State);

        UpdateProcess(m_State);
    }

    #endregion //유니티 생명주기

    #region 참조
    public Rigidbody2D m_Rigidbody2D;
    #endregion //참조

    #region 상태값
    public float m_MovementSpeed = 5f;
    public float m_JumpSpeed = 10f;

    public bool m_IsGround;
    #endregion //상태값

    #region 입력값
    public float m_xAxis;
    public float m_yAxis;

    #endregion //입력값

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    foreach(var contact in collision.contacts)
    //    {
    //        if(contact.collider.tag == "Ground" && contact.normal.y > 0.8f)
    //        {
    //            m_IsGround = true;
    //        }
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{

    //}





    //private void FixedUpdate()
    //{
    //    var xAxis = Input.GetAxis("Horizontal");
    //    var yAxis = Input.GetAxis("Vertical");

    //    Vector2 velocity = m_Rigidbody2D.velocity;
    //    velocity.x = xAxis * m_MovementSpeed;

    //    if (yAxis > 0.1f)
    //    {
    //        velocity.y = m_JumpSpeed;
    //    }
    //    m_Rigidbody2D.velocity = velocity;
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    foreach(var contact in collision.contacts)
    //    {
    //        //머리로 무언가에 부닥쳤을 때
    //        if (contact.normal.y > -0.8f)
    //        {
    //            Vector2 velocity = m_Rigidbody2D.velocity;

    //            //점프 중이면
    //            if (velocity.y > 0)
    //                velocity.y = 0;

    //            m_Rigidbody2D.velocity = velocity;
    //        }
    //    }
    //}


}
