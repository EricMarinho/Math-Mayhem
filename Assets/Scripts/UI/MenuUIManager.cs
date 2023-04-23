using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scenes;
using System.Linq;

namespace UI{
    public class MenuUIManager : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button returnToTittleScreenButton;
        [SerializeField] private List<Button> stageButtons;

        [Header("Canvas")]
        [SerializeField] private GameObject tittleScreenCanvas;
        [SerializeField] private GameObject stageSelectCanvas;

        private void Start()
        {
            startGameButton.onClick.AddListener(StartGame);
            returnToTittleScreenButton.onClick.AddListener(ReturnToTittleScreen);
            foreach (var (stageButton, i) in stageButtons.Select((value, i) => (value, i)))
            {
                stageButton.onClick.AddListener(() => SceneManagerHandler.Instance.LoadStage(i+1));
            }   
        }

        private void OnEnable()
        {
            
        }

        private void StartGame()
        {
            tittleScreenCanvas.SetActive(false);
            stageSelectCanvas.SetActive(true);
        }

        private void ReturnToTittleScreen()
        {
            tittleScreenCanvas.SetActive(true);
            stageSelectCanvas.SetActive(false);
        }


    }

}