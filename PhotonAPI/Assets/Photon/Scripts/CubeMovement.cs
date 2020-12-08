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

    // Update is called once per frame
    void Update()
    {
        if (m_PhotonView.IsMine)
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
    }

    private void OnMouseDown()
    {
        m_PhotonView.RPC("ColorRandomSwitch", RpcTarget.AllBuffered);
    }

    [PunRPC]

    public void ColorRandomSwitch()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = new Color(Random.Range(0, 1f),
            Random.Range(0, 1f), Random.Range(0, 1f));
    }


}
