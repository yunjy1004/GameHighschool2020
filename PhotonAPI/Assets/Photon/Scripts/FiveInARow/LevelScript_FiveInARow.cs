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

public class LevelScript_FiveInARow :MonoBehaviour
{
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
    }




    public void LetGoOfTheHorse(Side side, Vector2Int pos)
    {
        if((side == Side.Red && m_Turn == Turn.Red)
            || (side == Side.Blue && m_Turn == Turn.Blue))
        {
            if(m_Board[pos.x, pos.y].m_Side == Side.Other)
            {
                m_Board[pos.x, pos.y].m_Side = side;

                if(side == Side.Red)
                    m_Board[pos.x, pos.y].SetColor(1, 0, 0);
                else
                    m_Board[pos.x, pos.y].SetColor(0, 0, 1);

                NextTurn();
            }
        }
    }

    public void NextTurn()
    {
        if(m_Turn == Turn.Red)
        {
            m_Turn = Turn.Blue;
        }
        else if(m_Turn == Turn.Blue)
        {
            m_Turn = Turn.Red;
        }
    }
}
