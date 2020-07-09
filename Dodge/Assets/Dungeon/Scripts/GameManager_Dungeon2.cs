 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Dungeon2 : MonoBehaviour
{
    public Transform m_StartPoint;
    public PlayerController_Dungeon2 m_Player;

    public Text m_ClearUI;
    public Text m_ScoreUI;

    private void Start()
    {
        GameStart();
    }
    public void GameStart()
    {
        //출발시점에서 플레이어가 스폰
        m_Player.gameObject.SetActive(true);
        m_Player.transform.position = m_StartPoint.position;
        m_Player.transform.rotation = m_StartPoint.rotation;
        //게임 클리어 메세지가 보이지 않는다.
        m_ClearUI.gameObject.SetActive(false);
        //게임 스코어 메시지가 보인다.
        m_ScoreUI.gameObject.SetActive(true);
    }

    public void GameClear()
    {
        //플레이어가 비활성화된다.
        //게임 클리어 메세지가 보인다.
        //게임 스코어 메세지가 보인다.
    }

    public void ReturnToStartPoint()
    {
        //플레이어를 출발지점으로 되돌린다.
        m_Player.transform.position = m_StartPoint.position;
        m_Player.transform.rotation = m_StartPoint.rotation;
    }
}
