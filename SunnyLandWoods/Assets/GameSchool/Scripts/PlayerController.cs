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

    #region 참조
    public Rigidbody2D m_Rigidbody2D;
    public Animator m_Animator;
    #endregion //참조

    #region 상태값
    public float m_MovementSpeed = 5f;
    public float m_JumpSpeed = 10f;

    public bool m_IsGround;
    public int m_AttachedGroundCount = 0;
    public List<GameObject> m_AttachedGround = new List<GameObject>();
    #endregion //상태값

    #region 입력값
    public float m_xAxis;
    public float m_yAxis;
    #endregion //입력값


    public void EnterState(State state)
    {
        m_Animator.ResetTrigger("Idle");
        m_Animator.ResetTrigger("Walking");
        m_Animator.ResetTrigger("Jumpping");

        switch (state)
        {
            case State.None:
                break;
            case State.Idle:
                {
                    m_Animator.SetTrigger("Idle");
                }
                break;
            case State.Walking:
                {
                    m_Animator.SetTrigger("Walking");
                }
                break;
            case State.Jumping:
                {
                    m_Animator.SetTrigger("Jumpping");
                }
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
                    if(Mathf.Abs(m_xAxis) <= 0.1f)
                    {
                        Vector2 velocity = m_Rigidbody2D.velocity;
                        velocity.x = 0;
                        m_Rigidbody2D.velocity = velocity;
                    }
                    else
                    {
                        ChangeState(State.Walking);
                    }
                    //위쪽 입력이 들어오고, 땅을 밝고 있을 때
                    if(m_yAxis >= 0.1f && m_AttachedGround.Count >= 1)
                    {
                        //플레이어는 위쪽으로 점프한다.
                        Vector2 velocity = m_Rigidbody2D.velocity;
                        velocity.y = m_JumpSpeed;
                        m_Rigidbody2D.velocity = velocity;
                        ChangeState(State.Jumping);
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


                    if (m_yAxis >= 0.1f && m_AttachedGround.Count >= 1)
                    {
                        Vector2 velocity = m_Rigidbody2D.velocity;
                        velocity.y = m_JumpSpeed;
                        m_Rigidbody2D.velocity = velocity;
                        ChangeState(State.Jumping);
                    }
                }
                break;
            case State.Jumping:
                {
                    //땅에 닫고 있고,
                    //떨어지고 있을 경우에
                    if(m_AttachedGroundCount >= 1 && m_Rigidbody2D.velocity.y <= 0)
                    {
                        if (Mathf.Abs(m_xAxis) <= 0.1f)
                            ChangeState(State.Idle);
                        else
                            ChangeState(State.Walking);
                    }
                }
                break;
            default:
                break;
        }
    }

    public SpriteRenderer m_Renderer;
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
                {
                    //스프라이트 플립
                    if (m_xAxis > 0)
                        m_Renderer.flipX = false;
                    else if(m_xAxis < 0)
                        m_Renderer.flipX = true;
                }
                break;
            case State.Jumping:
                {
                    //스프라이트 플립
                    if (m_xAxis > 0)
                        m_Renderer.flipX = false;
                    else if (m_xAxis < 0)
                        m_Renderer.flipX = true;
                }
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

        //디버깅용
        m_AttachedGroundCount = m_AttachedGround.Count;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            //부닥친 모든 부분 중
            foreach (var contact in collision.contacts)
            {
                //밟은 부분이 아래라면
                if (contact.normal.y >= 1)
                {
                    //밟고 있는 땅에 대한 중복처리
                    if (!m_AttachedGround.Contains(collision.collider.gameObject))
                        m_AttachedGround.Add(collision.collider.gameObject);

                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            //밟고 있는 땅에 대한 중복처리
            if (m_AttachedGround.Contains(collision.collider.gameObject))
                m_AttachedGround.Remove(collision.collider.gameObject);
        }
    }

    #endregion //유니티 생명주기



    //public int m_ContactGround;

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    foreach (var contact in collision.contacts)
    //    {
    //        if(contact.collider.tag == "Ground"
    //            && contact.normal.y > 0.8f)
    //        {
    //            m_IsGround = true;
    //        }
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    foreach (var contact in collision.contacts)
    //    {
    //        if (contact.collider.tag == "Ground"
    //            && contact.normal.y > 0.8f)
    //        {
    //            m_IsGround = false;
    //        }
    //    }
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
