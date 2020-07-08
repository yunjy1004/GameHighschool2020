using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap3 : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody != null)
        {
            var player = collision.rigidbody.GetComponent<PlayerController_Dungeon2>();
            if (player != null)
                player.Die();
        }
        Debug.Log("Collision이 어떤 Collision과 충돌이 일어 났을때");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision이 어떤 Collision과 충돌이 끝났을때");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision이 어떤 Collision과 충돌이 일어나고 있음");
    }

    public void Die()
    {
        Debug.Log("사망");
        gameObject.SetActive(false);
        //m_GameManager.GameOver();
    }

}
