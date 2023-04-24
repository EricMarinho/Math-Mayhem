using ObjectPool;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Quizz
{
    public class AnswersManager : MonoBehaviour
    {

        [SerializeField] private QuizzData quizzData ;
        public List<int> currentQuestionNumbers { get; private set; } = null;
        public int currentAnswer { get; private set; }
        public List<int> currentPossibleAnswers { get; private set; }
        public string currentOperator { get; private set; } = null;
        public int currentAnswersQuantity { get; private set; }
        private int currentQuestionIndex = 0;
        private bool isFinished = false;

        private QuizzUIHandler quizzUIHandlerInstance;
        public static AnswersManager instance { get; private set; }
        private void Awake()
        {
            instance = this;

            currentPossibleAnswers = new List<int>();
            currentQuestionNumbers = new List<int>();
        }

        private void Start()
        {
            LifeHandler.instance.OnGameOver += ReturnAnswersToPool;
            quizzUIHandlerInstance = QuizzUIHandler.instance;
            NextQuestion();
        }

        public void NextQuestion()
        {
            if (isFinished)
            {
                EndGame();
                return;
            }

            ReturnAnswersToPool();

            if (quizzData.questions[currentQuestionIndex].isRandomQuestion)
            {

                quizzUIHandlerInstance.currentSpawnedAnswers.Clear();
                currentAnswersQuantity = quizzData.questions[currentQuestionIndex].answersQuantity;
                currentQuestionNumbers = GetRandomQuestionNumber(quizzData.questions[currentQuestionIndex].operationNumbers);

                switch (quizzData.questions[currentQuestionIndex].operationOperator)
                {
                    case Operator.Plus:
                        currentAnswer = GetSum(currentQuestionNumbers);
                        currentOperator = "+";
                        break;

                    case Operator.Minus:
                        currentAnswer = GetSubstraction(currentQuestionNumbers);
                        currentOperator= "-";
                        break;

                    case Operator.Multiply:
                        currentAnswer = GetMultiplication(currentQuestionNumbers);
                        currentOperator= "*";
                        break;

                    case Operator.Divide:
                        currentAnswer = GetDivision(currentQuestionNumbers);
                        currentOperator= "/";
                        break;

                    default:
                        Debug.Log("Invalid Operation");
                        break;
                }
                
                SetPossibleAnswers();
                QuizzUIHandler.instance.SetNewAnswers();
                QuizzUIHandler.instance.SetQuestionText();

            }
            else
            {
                Debug.Log("Not random question");
            }
            currentQuestionIndex++;
            if (currentQuestionIndex >= quizzData.questions.Count)
            {
                isFinished = true;
            }
        }

        public void ReturnAnswersToPool()
        {
            foreach (GameObject answer in quizzUIHandlerInstance.currentSpawnedAnswers)
            {
                quizzUIHandlerInstance.poolSpawner.ReturnToPool("Answer", answer);
            }
        }

        private void SetPossibleAnswers()
        {
            currentPossibleAnswers.Clear();
            currentPossibleAnswers.Add(currentAnswer);
            int multiplier = 1;
            for(int i = 1; i < currentAnswersQuantity; i++)
            {
                currentPossibleAnswers.Add(currentAnswer + (multiplier * Random.Range(1, quizzData.questions[currentQuestionIndex].answerMarginError)));
                multiplier *= -1;
            }
            
            ShuffleList();
        }

        private void ShuffleList()
        {
            int n = currentPossibleAnswers.Count;
            while (n > 1)
            {
                n--;
                int k = (int)Random.Range(0, n + 1);
                int value = currentPossibleAnswers[k];
                currentPossibleAnswers[k] = currentPossibleAnswers[n];
                currentPossibleAnswers[n] = value;
            }
        }

        private int GetSum(List<int> operationNumbers)
        {
            return operationNumbers[0] + operationNumbers[1];
        }

        private int GetSubstraction(List<int> operationNumbers)
        {
            return operationNumbers[0] - operationNumbers[1];
        }

        private int GetMultiplication(List<int> operationNumbers)
        {
            return operationNumbers[0] * operationNumbers[1];
        }

        private int GetDivision(List<int> operationNumbers)
        {
            return operationNumbers[0] / operationNumbers[1];
        }

        private List<int> GetRandomQuestionNumber(List<MinMax> minMaxNumber)
        {
            List<int> result = new List<int>
        {
            Random.Range(minMaxNumber[0].minValue, minMaxNumber[0].maxValue),
            Random.Range(minMaxNumber[1].minValue, minMaxNumber[1].maxValue)
        };
            return result;
        }

        private void EndGame()
        {
            QuizzUIHandler.instance.WinGameUI();
        }
    }
}