using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class CubeMovement : MonoBehaviour
{
    PhotonView m_PhotonView;
    // Start is called before the first frame update
    void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    private Vector3 m_OldPosition;
    void Update()
    {
        if (m_PhotonView.IsMine || !m_PhotonView.IsMine)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                transform.position += transform.up * Time.deltaTime;
            if (Input.GetKey(KeyCode.DownArrow))
                transform.position -= transform.up * Time.deltaTime;
            if (Input.GetKey(KeyCode.RightArrow))
                transform.position += transform.right * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftArrow))
                transform.position -= transform.right * Time.deltaTime;
        }

        if(m_OldPosition != transform.position)
        {
            m_PhotonView.RPC("Move", RpcTarget.AllBuffered, 
                transform.position, transform.rotation);
        }

        m_OldPosition = transform.position;
    }
    [PunRPC]
    public void Move(Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
        m_OldPosition = transform.position;
    }

    private void OnMouseDown()
    {
        float r, g, b;
        
        r = Random.Range(0, 1f);
        g = Random.Range(0, 1f);
        b = Random.Range(0, 1f);

        m_PhotonView.RPC("ColorRandomSwitch", RpcTarget.AllBuffered, r, g, b);
    }

    [PunRPC]
    public void ColorRandomSwitch(float r, float g, float b)
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = new Color(r, g, b);
    }

}
