using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlueCube : MonoBehaviour, IPointerDownHandler
{
    public float m_Speed = 5f;

    void Update()
    {
        var movement = Vector3.down * m_Speed * Time.deltaTime;
        transform.position += movement;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("파랑 큐브 누름");
        Destroy(gameObject);
        GameManager.Instance.DamageLife();
        //throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "plane")
        {
            Destroy(gameObject);
            GameManager.Instance.AddScore();
        }
    }
}
