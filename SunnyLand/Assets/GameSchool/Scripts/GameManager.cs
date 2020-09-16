using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<ItemComponent> m_Items = new List<ItemComponent>();

    public GameObject m_GameClearUI;

    public bool m_IsGameOver;
    public GameObject m_GameOverUI;

    public GameObject m_Player;
    public Transform m_StartPoint;

    public JointArm m_JointArm;

    public bool m_IsPlaying;

    public void GameStart()
    {
        var playerInstance = Instantiate(m_Player, m_StartPoint.position, m_StartPoint.rotation);

        var hpComponent = playerInstance.GetComponent<HPComponent>();
            hpComponent.m_OnDie.AddListener(GameOver);

        m_JointArm.m_Target = playerInstance.transform;
    }

    public void Start()
    {
        m_Items.AddRange(FindObjectsOfType<ItemComponent>());
        m_IsPlaying = true;

        GameStart();
    }

    public void GameOver()
    {
        m_IsGameOver = true;
        m_GameOverUI.SetActive(true);
    }

    public void GameClear()
    {
        m_GameClearUI.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("GameClear");
    }

    public void Update()
    {

        if (m_IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
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
}
