using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CubeSpawner m_Cubespawner;

    public int m_Life = 3;
    public int m_Score = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        m_Cubespawner.SpawnStart();
    }

    public void AddScore()
    {
        m_Score++;
    }

    public void DamageLife()
    {
        m_Life--;
        if(m_Life <= 0)
        {
            //Gameover
            m_Cubespawner.gameObject.SetActive(false);
        }
    }
}
