using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RedCube : MonoBehaviour, IPointerDownHandler
{
    public float m_Speed = 5;

    void Update()
    {
        var movement = Vector3.down * m_Speed * Time.deltaTime;
        transform.position += movement;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("빨간 큐브 누름");
        Destroy(gameObject);
        GameManager.Instance.AddScore();
        //throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "plane")
        {
            GameManager.Instance.DamageLife();
            Destroy(gameObject);
        }
    }
}
