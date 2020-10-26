﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        None = 0,    //초기 상태
        Idle,
        Walking,
        Jumping,
        Crouching,
    }

    public State m_State = State.None;

    #region 참조
    public Rigidbody2D m_Rigidbody2D;
    public Animator m_Animator;
    public Collider2D m_MovementCollider;
    public Collider2D m_SideBlockCollider;
    #endregion //참조

    #region 상태값
    public float m_MovementSpeed = 5f;
    public float m_JumpSpeed = 10f;

    public bool m_IsGround;

    public float m_CrouchTimer = 0;
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
        m_Animator.ResetTrigger("Crouch");

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
            case State.Crouching:
                {
                    m_Animator.SetTrigger("Crouch");
                    m_CrouchTimer = 0f;
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
            case State.Crouching:
                m_MovementCollider.enabled = true;
                m_SideBlockCollider.enabled = true;
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
            case State.Crouching:
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
                    //위쪽 입력이 들어오고, 땅을 밝고 있을 때
                    if (m_IsGround && m_yAxis >= 0.1f)
                    {
                        //플레이어는 위쪽으로 점프한다.
                        Vector2 velocity = m_Rigidbody2D.velocity;
                        velocity.y = m_JumpSpeed;
                        m_Rigidbody2D.velocity = velocity;
                        ChangeState(State.Jumping);
                    }

                    if (m_IsGround && m_yAxis <= -0.2f)
                    {
                        ChangeState(State.Crouching);
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


                    if (m_IsGround && m_yAxis >= 0.1f)
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
                    if (Mathf.Abs(m_xAxis) > 0.1f)
                    {
                        Vector2 velocity = m_Rigidbody2D.velocity;
                        velocity.x = m_xAxis * m_MovementSpeed;
                        m_Rigidbody2D.velocity = velocity;
                    }

                    if (m_IsGround)
                    {
                        if (Mathf.Abs(m_xAxis) <= 0.1f)
                            ChangeState(State.Idle);
                        else
                            ChangeState(State.Walking);
                    }
                }
                break;
            case State.Crouching:
                {
                    if (m_yAxis >= 0)
                    {
                        ChangeState(State.Idle);
                    }

                    m_CrouchTimer += Time.fixedDeltaTime;

                    if (m_CrouchTimer >= 2f && m_CrouchTimer <= 90f)
                    {
                        m_MovementCollider.enabled = false;
                        m_SideBlockCollider.enabled = false;

                        m_CrouchTimer = 100f;
                    }
                    else if (m_CrouchTimer >= 100.3f)
                    {
                        m_MovementCollider.enabled = true;
                        m_SideBlockCollider.enabled = true;
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
                    else if (m_xAxis < 0)
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
    }

    #endregion //유니티 생명주기

    public void SetIsGround(bool isGround)
    {
        m_IsGround = isGround;
    }

}