using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour{
    public GameObject m_Prefab;
    public Vector3Int m_grid;
    public float m_gap;
    [ContextMenu("gridSpawn")]
    public void GridSpawn()
    {
        GameObject parent = new GameObject();
        Vector3 parentPos = new Vector3();
        parentPos.x = (m_grid.x * m_gap - 1) / 2f;
        parentPos.y = (m_grid.y * m_gap - 1) / 2f;
        parentPos.z = (m_grid.z * m_gap - 1) / 2f;
        parent.transform.position = parentPos;
        for (int x =0; x<m_grid.x; x++)
        {
            for (int y = 0; y < m_grid.y; y++)
            {
                for (int z = 0; z < m_grid.z; z++)
                {
                    Vector3 pos = new Vector3(x * m_gap,
                        y * m_gap, 
                        z * m_gap);
                    var gobj = Instantiate(m_Prefab, 
                        pos, 
                        Quaternion.identity, 
                        parent.transform);

                    var p = gobj.GetComponent<Point>();
                    if (p){
                        p.m_Point = new Vector2Int(x, y);
                    }
                }
            }
        }

        parent.transform.position = Vector3.zero;
    }
}
