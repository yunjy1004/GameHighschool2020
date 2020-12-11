using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class LevelScript_FiveInARow : MonoBehaviour
{
    public Turn m_Turn;

    public Point[,] m_Board = new Point[21, 21];

    //추가
    public void Start()
    {
        var points = FindObjectsOfType<Point>();
        foreach (var point in points)
        {
            m_Board[point.m_Point.x, point.m_Point.y]
                = point;

            point.m_Side = Side.Other;

            point.SetColor(1, 1, 1);
        }
    }




    public void LetGoOfTheHorse(Side side, Vector2Int pos)
    {
        if ((side == Side.Red && m_Turn == Turn.Red)
            || (side == Side.Blue && m_Turn == Turn.Blue))
        {
            if (m_Board[pos.x, pos.y].m_Side == Side.Other)
            {
                m_Board[pos.x, pos.y].m_Side = side;

                if (side == Side.Red)
                    m_Board[pos.x, pos.y].SetColor(1, 0, 0);
                else
                    m_Board[pos.x, pos.y].SetColor(0, 0, 1);

                if (CheckVictory(side, pos))
                {
                    Debug.Log("victory : " + side);
                }

                NextTurn();
            }
        }
    }

    public void NextTurn()
    {
        if (m_Turn == Turn.Red)
        {
            m_Turn = Turn.Blue;
        }
        else if (m_Turn == Turn.Blue)
        {
            m_Turn = Turn.Red;
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
                if (m_Board[pos.x + i, pos.y].m_Side != side)
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
                if (m_Board[pos.x, pos.y - i].m_Side != side)
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
                if (m_Board[pos.x - i, pos.y].m_Side != side)
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
                if (m_Board[pos.x + i, pos.y + i].m_Side != side)
                {
                    break;
                }

                rowCount++;
            }

            //위로 몇 번이나 반복되는지
            for (int i = 1; i < 5; i++)
            {
                if (!InBoardRange(pos.x - i, pos.y - i))
                {
                    break;
                }
                if (m_Board[pos.x - i, pos.y - i].m_Side != side)
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
                if (m_Board[pos.x + i, pos.y - i].m_Side != side)
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
                if (m_Board[pos.x - i, pos.y + i].m_Side != side)
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
}
