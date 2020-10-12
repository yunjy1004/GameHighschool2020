using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger2D : MonoBehaviour
{
    [System.Flags]
    public enum Mask
    {
        None = 0,               //000000
        Player = ( 1 << 0 ),    //000001
        Enemy  = ( 1 << 1 ),    //000010
        Ect    = ( 1 << 2 ),    //000100
        All = Player | Enemy,   //000011
    }

    public Mask m_Mask;

    public UnityEvent m_OnTriggerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((m_Mask & Mask.Player) == Mask.Player)   //플러그 참일 경우 처리,
        {
            if (collision.attachedRigidbody.tag == "Player")
                m_OnTriggerEnter.Invoke();
        }
    }
}
