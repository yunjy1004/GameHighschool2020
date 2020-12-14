using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_FiveARow 
/*수정*/    : Photon.Pun.MonoBehaviourPun
{
    public Side m_Side;
    private LevelScript_FiveInARow m_LevelScript;

    public void Awake(){
        m_LevelScript = FindObjectOfType<LevelScript_FiveInARow>();
    }
    public void Update(){
        if (!photonView.IsMine) return;

        if (Input.GetMouseButtonDown(0))
        {
            //UI에 대한 블럭처리.
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //카메라 오브젝트를 선택하려고 할 때에
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
                {
                    //내가 보드판에서 칸을 선택해다면
                    if (hit.collider != null)
                    {
                        var point = hit.collider.GetComponent<Point>();
                        if (point != null)
                        {
                            photonView.RPC("LetGoOfTheHorse", RpcTarget.MasterClient, 
                                (int)m_Side, 
                                point.m_Point.x, point.m_Point.y);
                            //m_LevelScript.LetGoOfTheHorse(m_Side, point.m_Point);
                        }
                    }
                }
            }
        }
    }
    [Photon.Pun.PunRPC]
    public void SetPlayerInfo(string playerName, int side)
    {
        gameObject.name = playerName;
        m_Side = (Side)side;
    }

    //추가
    [PunRPC]
    public void LetGoOfTheHorse(int side, int posX, int posY)
    {
        m_LevelScript.LetGoOfTheHorse((Side)side, new Vector2Int(posX, posY));
    }
}
