using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Trap2 : MonoBehaviour
{

    public UnityEvent m_OnEnter;
    public Text m_ClearUI;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거 안에 어떤 Collider나 Trigger가 들어갔을 때");
        if(other.attachedRigidbody != null)
        {
            var player = other.attachedRigidbody.GetComponent<PlayerController_Dungeon2>();
            if (player != null)
                m_OnEnter.Invoke();
            m_ClearUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("트리거 안에 어떤 Collider나 Trigger가 나왔을 때");
        //스폰
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("트리거 안에 어떤 Collider나 Trigger가 들어가있는 도중");
    }

}
