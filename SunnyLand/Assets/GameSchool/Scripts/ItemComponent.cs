using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemComponent : MonoBehaviour
{
    public UnityEvent m_PickUpItemEvent;

    public virtual void PickUpItem()
    {
        //아이템 이벤트 처리
        m_PickUpItemEvent.Invoke();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
