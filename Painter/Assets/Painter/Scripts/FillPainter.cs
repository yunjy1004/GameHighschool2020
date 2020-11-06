using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillPainter : MonoBehaviour
{
    public GameObject m_Canvas;

    public RenderTexture m_RendererTexture;
    public Texture2D m_CanvasTexture;

    public bool[] m_PixelCheckBoard;
    public void Start()
    {
        var renderer = m_Canvas.GetComponent<Renderer>();
        var texture2D = (Texture2D)renderer.material.mainTexture;
        m_CanvasTexture = new Texture2D(texture2D.width, texture2D.height);

        renderer.material.mainTexture = m_CanvasTexture;

        m_CanvasTexture.SetPixels(texture2D.GetPixels());
        m_CanvasTexture.Apply();

        m_PixelCheckBoard = new bool[m_CanvasTexture.width * m_CanvasTexture.height];
    }

    public void ClearCheckBoard()
    {
        for (int i = 0; i < m_PixelCheckBoard.Length; i++)
        {
            m_PixelCheckBoard[i] = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fill(300, 512, Color.red);
        }
    }

    public void Fill(int x, int y, Color color)
    {
        ClearCheckBoard();

        Color oriColor = m_CanvasTexture.GetPixel(x, y);

        FillProcessQueue(x, y, color, oriColor);

        m_CanvasTexture.Apply();
    }

    public void FillProcessRecursive(int x, int y, Color color, Color oriColor)
    {
        //이미 지나친 픽셀의 경우
        if (m_PixelCheckBoard[x + y * m_CanvasTexture.width])
                return;

        //픽셀 체크
        m_PixelCheckBoard[x + y * m_CanvasTexture.width] = true;

        if(m_CanvasTexture.GetPixel (x,y) == oriColor)
        {
            m_CanvasTexture.SetPixel(x, y, color);

            FillProcessRecursive(x + 1, y, color, oriColor);
            FillProcessRecursive(x - 1, y, color, oriColor);
            FillProcessRecursive(x, y + 1, color, oriColor);
            FillProcessRecursive(x, y - 1, color, oriColor);
        }

    }

    public void FillProcessQueue(int x, int y, Color color, Color oriColor)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        queue.Enqueue(new Vector2Int(x, y));

        while (queue.Count > 0)
        {
            Vector2Int pos = queue.Dequeue();

            if(!m_PixelCheckBoard[pos.x + pos.y * m_CanvasTexture.width])
            {
                m_PixelCheckBoard[pos.x + pos.y * m_CanvasTexture.width] = true;

                if(m_CanvasTexture.GetPixel(pos.x, pos.y) == oriColor)
                {
                    m_CanvasTexture.SetPixel(pos.x, pos.y, color);

                    queue.Enqueue(new Vector2Int(pos.x + 1, pos.y));
                    queue.Enqueue(new Vector2Int(pos.x - 1, pos.y));
                    queue.Enqueue(new Vector2Int(pos.x, pos.y + 1));
                    queue.Enqueue(new Vector2Int(pos.x, pos.y - 1));
                }
            }
        }
    }


    
}
