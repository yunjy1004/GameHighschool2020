using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.UI;

public enum Side
{
    Red,    //레드 플레이어 소속 
    Blue,   //블루 플레이어 소속
    Other   //기타 플레이어 소속(아마 관전자인데 안쓸것)
}

public enum Turn
{
    Red,    //레드 턴 
    Blue    //블루 턴
}

public class LevelScript_FiveInARow : 
    MonoBehaviourPunCallbacks
{

    public Text m_PlayerUI;
    public Text m_TurnUI;
    public GameObject m_WinnerPanel;
    public Text m_WinnerUI;

    public Turn m_Turn;

    public Point[,] m_Board = new Point[21, 21];

    //추가
    public void Start()
    {
        var points = FindObjectsOfType<Point>();
        foreach(var point in points)
        {
            m_Board[point.m_Point.x, point.m_Point.y]
                = point;

            point.m_Side = Side.Other;

            point.SetColor(1,1,1);
        }


        //추가

        //새로운 플레이어 인스턴트를 생성.
        var playerObj = PhotonNetwork.Instantiate(
            "Player",
            Vector3.zero,
            Quaternion.identity);

        var player =
        playerObj.GetComponent<Player_FiveARow>();

        //새로 입장한 플레이어가 자신이라면,
        if (PhotonNetwork.IsMasterClient)
        {
            //수정
            player.photonView.RPC("SetPlayerInfo", RpcTarget.AllBuffered, 
                "Player_Red", (int)Side.Red);

            m_PlayerUI.text = "Player Red";
        }
        else
        {
            //수정
            player.photonView.RPC("SetPlayerInfo", RpcTarget.AllBuffered,
                "Player_Blue", (int)Side.Blue);

            m_PlayerUI.text = "Player Blue";
        }

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SetTurnUI", RpcTarget.AllBuffered, (int)m_Turn);
            //시작했을때 2사림이 입장을 안했다면 레디
            photonView.RPC("SetWinnerUI", RpcTarget.AllBuffered, true, "Ready!");
        }
    }


    //이 함수는 플레이어가 새로 룸에 입장했을 때 호출이되요.
    public override void OnPlayerEnteredRoom(
        Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        //해당 PC가 호스트일 경우,
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.PlayerList.Length == 2)
        {
            //2사람이 입장했다면 Panel제거해서 게임이 시작
            photonView.RPC("SetWinnerUI", RpcTarget.AllBuffered, false, "Ready!");
        }
    }

    public void LetGoOfTheHorse(Side side, Vector2Int pos)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if((side == Side.Red && m_Turn == Turn.Red)
            || (side == Side.Blue && m_Turn == Turn.Blue))
        {
            if(m_Board[pos.x, pos.y].m_Side == Side.Other)
            {
                m_Board[pos.x, pos.y].m_Side = side;

                if(side == Side.Red)
                    m_Board[pos.x, pos.y].photonView.RPC("SetColor", 
                        RpcTarget.AllBuffered,
                        1f, 0f, 0f);
                else
                    m_Board[pos.x, pos.y].photonView.RPC("SetColor",
                        RpcTarget.AllBuffered,
                        0f, 0f, 1f);

                if (CheckVictory(side, pos))
                {
                    Debug.Log("victory : " + side);

                    //승패가 나면 Panel 활성화해주고 누가 승리했는지 출력
                    photonView.RPC("SetWinnerUI", RpcTarget.AllBuffered, true, "Winner : " + side.ToString());
                }

                NextTurn();
            }
        }
    }

    public void NextTurn()
    {
        if(m_Turn == Turn.Red)
        {
            m_Turn = Turn.Blue;
            photonView.RPC("SetTurnUI", RpcTarget.AllBuffered, (int)m_Turn);
        }
        else if(m_Turn == Turn.Blue)
        {
            m_Turn = Turn.Red;
            photonView.RPC("SetTurnUI", RpcTarget.AllBuffered, (int)m_Turn);
        }
    }

    //수정
    public bool CheckVictory(Side side, Vector2Int pos)
    {
        //삼방에

        //상하에 대해서 체크
        {
            int rowCount = 1;

            //위로 몇 번이나 반복되는지
            for (int i = 1; i < 5; i++)
            {
                if (!InBoardRange(pos.x + i, pos.y))
                {
                    break;
                } 
                if(m_Board[pos.x + i, pos.y].m_Side != side)
                {
                    break;
                }

                rowCount++;
            }

            //위로 몇 번이나 반복되는지
            for (int i = 1; i < 5; i++)
            {
                if (!InBoardRange(pos.x, pos.y - i))
                {
                    break;
                }
                if(m_Board[pos.x, pos.y - i].m_Side != side)
                {
                    break;
                }

                rowCount++;
            }

            if (rowCount >= 5)
            {
                return true;
            }
        }

        //좌우에 대해서 체크
        {
            int rowCount = 1;

            //위로 몇 번이나 반복되는지
            for (int i = 1; i < 5; i++)
            {
                if (!InBoardRange(pos.x + i, pos.y)) 
                {
                    break;
                }
                if (m_Board[pos.x + i, pos.y].m_Side != side)
                {
                    break;
                }

                rowCount++;
            }

            //위로 몇 번이나 반복되는지
            for (int i = 1; i < 5; i++)
            {
                if (!InBoardRange(pos.x - i, pos.y))
                {
                    break;
                }
                if( m_Board[pos.x - i, pos.y].m_Side != side)
                {
                    break;
                }

                rowCount++;
            }

            if (rowCount >= 5)
            {
                return true;
            }
        }

        //대각선에 대한 체크
        {
            int rowCount = 1;

            //위로 몇 번이나 반복되는지
            for (int i = 1; i < 5; i++)
            {
                if (!InBoardRange(pos.x + i, pos.y + i))
                {
                    break;
                }
                if(m_Board[pos.x + i, pos.y + i].m_Side != side)
                {
                    break;
                }

                rowCount++;
            }

            //위로 몇 번이나 반복되는지
            for (int i = 1; i < 5; i++)
            {
                if (!InBoardRange(pos.x - i, pos.y - i)) {
                    break;
                }
                if(m_Board[pos.x - i, pos.y - i].m_Side != side)
                {
                    break;
                }

                rowCount++;
            }

            if (rowCount >= 5)
            {
                return true;
            }
        }

        //역대각선에 대한 체크
        {
            int rowCount = 1;

            //위로 몇 번이나 반복되는지
            for (int i = 1; i < 5; i++)
            {
                if (!InBoardRange(pos.x + i, pos.y - i))
                {
                    break;
                }
                if(m_Board[pos.x + i, pos.y - i].m_Side != side)
                {
                    break;
                }

                rowCount++;
            }

            //위로 몇 번이나 반복되는지
            for (int i = 1; i < 5; i++)
            {
                if (!InBoardRange(pos.x - i, pos.y + i))
                {
                    break;
                }
                if(m_Board[pos.x - i, pos.y + i].m_Side != side)
                {
                    break;
                }

                rowCount++;
            }

            if (rowCount >= 5)
            {
                return true;
            }
        }

        return false;
    }

    public bool InBoardRange(int x, int y)
    {
        bool result = true;
        if (x < 0 || x >= m_Board.GetLength(0))
        {
            result &= false;
        }

        if (y < 0 || y >= m_Board.GetLength(1))
        {
            result &= false;
        }

        return result;
    }

    [PunRPC]
    public void SetTurnUI(int side)
    {
        m_TurnUI.text = string.Format("Turn : {0}", ((Side)side).ToString());
    }

    [PunRPC]
    public void SetWinnerUI(bool activity, string message)
    {
        m_WinnerPanel.SetActive(activity);
        m_WinnerUI.text = message;
    }
}
