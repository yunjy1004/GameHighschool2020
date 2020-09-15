using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<ItemComponent> m_Items = new List<ItemComponent>();

    public GameObject m_GameClearUI;

    public bool m_IsPlaying;

    public void Start()
    {
        m_Items.AddRange(FindObjectsOfType<ItemComponent>());
        m_IsPlaying = true;
    }

    public void GameClear()
    {
        m_GameClearUI.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("GameClear");
    }

    public void Update()
    {
        if (!m_IsPlaying) return;

        bool result = true;
        foreach(var item in m_Items)
        {
            if (item != null)
                result = false;
        }

        if (result)
        {
            m_IsPlaying = false;
            GameClear();
        }
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void GameStart()
    {

    }
}
