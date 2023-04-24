using ObjectPool;
using Player;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Quizz {
    public class QuizzOption : MonoBehaviour
    {
        public int optionNumber;
        QuizzUIHandler quizzUIHandlerInstance;

        private void Start()
        {
            quizzUIHandlerInstance = QuizzUIHandler.instance;

            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (optionNumber == AnswersManager.instance.currentAnswer)
                {
                    AnswersManager.instance.NextQuestion();
                }
                else
                {
                    LifeHandler.instance.LoseHealth();
                }
            });
        }
    }
}