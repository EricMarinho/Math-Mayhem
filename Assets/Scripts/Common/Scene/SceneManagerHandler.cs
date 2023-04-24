using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneManagerHandler : MonoBehaviour
    {
        public static SceneManagerHandler Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else Destroy(gameObject);
        }

        public void LoadStage(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
            PlayerPrefs.SetString("ReturnToStageSelect", "true");
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
