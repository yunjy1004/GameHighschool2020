using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickableobject : MonoBehaviour, IPointerDownHandler
{

    //private void OnMouseDown()
    //{

    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("오브젝트를 누름");
        //throw new System.NotImplementedException();
    }
}
