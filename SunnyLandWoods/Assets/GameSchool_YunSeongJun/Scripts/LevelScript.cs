using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YunSeongJun
{
    public class LevelScript : MonoBehaviour
    {
        public void RestartLevel()
        {
            var nowScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(nowScene.name);
        }

        public void LoadLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
