using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour
{

    //	 2020 - 11 - 03 수요일 FloodFill


    Vector3 mpos;
    public float pixelcount;
    public Texture2D tex;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mpos = Input.mousePosition;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mpos);
            Physics.Raycast(ray, out hit);


            Checkpoint(Color.white, Color.red, tex, (int)(tex.width * hit.textureCoord.x), (int)(tex.height * hit.textureCoord.y));


        }

    }

    void Checkpoint(Color oldColor, Color replacementColor, Texture2D tex, int x, int y)
    {


        if (oldColor == replacementColor) return;
        if (tex.GetPixel(x, y) != Color.white) return;

        else
        {
            pixelcount += 1;
            tex.SetPixel(x, y, Color.red);

            Checkpoint(Color.white, Color.red, tex, x + 1, y);
            //Checkpoint(Color.white, Color.red, tex, x, y + 1);
            //Checkpoint(Color.white, Color.red, tex, x - 1, y);
            //Checkpoint(Color.white, Color.red, tex, x, y - 1);

            tex.Apply();

        }

    }

}









    //방법2

    //        if (oldColor == replacementColor) return;
    //    if (tex.GetPixel(x, y) != oldColor) return;

    //    Queue<Coord> queue = new Queue<Coord>();

    //Coord kCoord;
    //kCoord.x = x;
    //    kCoord.y = y;

    //    queue.Enqueue(kCoord);

    //    while (queue.Count != 0) 
    //    {
    //        Coord n = queue.Dequeue();
    //        if (tex.GetPixel(x, y) == oldColor)
    //        {
    //            int _y = n.y;
    //int _w = n.x;
    //int _e = n.x;
    //            while (_w > 0 && tex.GetPixel(_w - 1, y) == oldColor) _w--;
    //            while (_e<tex.width - 1 && tex.GetPixel(_e + 1, y) == oldColor) _e++;


    //            for (int _x = _w; x <= _e; _x++)
    //            {
    //                tex.SetPixel(x, y, replacementColor);
    //                if(y>0 && tex.GetPixel(x, y-1) == oldColor)
    //                  {
    //                    queue.Enqueue(new Coord(x, y - 1));
    //                  }
    //                if(y<tex.width-1&&tex.GetPixel(x, y+1) == oldColor)
    //                {
    //                    queue.Enqueue(new Coord(x, y + 1));
    //                }

    //            }
    //        }

