using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public void Awake()
    {
        if (GameManager.Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        m_ScoreUI.text = string.Format("Score : {0}", m_Score);
    }

    public bool m_IsGameOver = false;
    public GameObject m_GameOverUI;
    public UnityEngine.UI.Text m_ScoreUI;
    public int m_Score = 0;

    // Start is called before the first frame update
    public void OnPlayerDead()
    {
        m_IsGameOver = true;
        m_GameOverUI.SetActive(true);

        ScrollingObject[] scrollingObjects = FindObjectsOfType<ScrollingObject>();
        foreach (var scrollingObject in scrollingObjects)
            scrollingObject.enabled = false;

        FindObjectOfType<PlatformSpawner>().enabled = false;
    }

    public void OnAddScore()
    {
        m_Score++;
        m_ScoreUI.text = string.Format("SCORE : {0}", m_Score);
    }
    // Update is called once per frame

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void Restart()
    {
        if (m_IsGameOver)
            SceneManager.LoadScene("Level_UniRun");
    }

    //private void Update()
    //{
    //    if(m_IsGameOver && Input.GetKeyDown(KeyCode.R))
    //    {
    //        SceneManager.LoadScene("Level_UniRun");
    //    }
    //}


}
