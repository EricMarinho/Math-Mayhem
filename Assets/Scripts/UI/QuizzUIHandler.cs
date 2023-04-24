using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Quizz;
using ObjectPool;
using Player;
using UnityEngine.UI;
using Scenes;

namespace UI { 
    public class QuizzUIHandler : MonoBehaviour
    {

        [SerializeField] private TMP_Text questionText;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text continueButtonText;
        [SerializeField] private Button continueButton;
        [SerializeField] private GameObject endGameCanvas;
        [SerializeField] private GameObject lifeCanvas;

        public PoolSpawner poolSpawner;

        public List<GameObject> currentSpawnedAnswers { get; private set;}
        private GameObject currentSpawnedAnswer;
        private AnswersManager answersManagerInstance;

        public static QuizzUIHandler instance { get; private set; }
        private void Awake()
        {
            instance = this;

            currentSpawnedAnswers = new List<GameObject>();
        }

        private void Start()
        {
            answersManagerInstance = AnswersManager.instance;
            LifeHandler.instance.OnGameOver += SetGameOverUI;
        }

        public void SetQuestionText()
        {
            
            questionText.SetText($"{answersManagerInstance.currentQuestionNumbers[0]} {answersManagerInstance.currentOperator} {answersManagerInstance.currentQuestionNumbers[1]} = ?");
        }

        public void SetNewAnswers()
        {
            for(int i = 0; i < answersManagerInstance.currentAnswersQuantity; i++)
            {
                currentSpawnedAnswer = poolSpawner.SpawnFromPool("Answer", transform.position, Quaternion.identity);
                currentSpawnedAnswer.GetComponentInChildren<TMP_Text>().SetText(answersManagerInstance.currentPossibleAnswers[i].ToString());
                currentSpawnedAnswer.GetComponent<QuizzOption>().optionNumber = answersManagerInstance.currentPossibleAnswers[i];
                currentSpawnedAnswers.Add(currentSpawnedAnswer);
            }
        }

        public void SetHealthUI(int health)
        {
            healthText.SetText("Lifes: " + health.ToString());
        }

        public void SetGameOverUI()
        {
            EndGame();
            questionText.SetText("Game Over");
            continueButtonText.SetText("Restart");
            continueButton.onClick.AddListener(()=>SceneManagerHandler.Instance.RestartScene());

        }

        public void WinGameUI()
        {
            EndGame();
            questionText.SetText("You Win");
            continueButtonText.SetText("Continue");
            continueButton.onClick.AddListener(() => SceneManagerHandler.Instance.ReturnToMenu());
        }

        public void EndGame()
        {
            endGameCanvas.gameObject.SetActive(true);
            lifeCanvas.gameObject.SetActive(false);
        }
    }
}
