using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Player
{
    public class LifeHandler : MonoBehaviour
    {
        #region Singleton
        public static LifeHandler instance { get; private set; }
        private void Awake()
        {
            instance = this;
        }
        #endregion

        [SerializeField] private PlayerData playerData;
        public Action OnGameOver;
        public int health { get; private set; }

        private void Start()
        {
            health = playerData.maxHealth;
            QuizzUIHandler.instance.SetHealthUI(health);
        }

        public void LoseHealth()
        {
            health--;
            if(health <= 0)
            {
                OnGameOver?.Invoke();
            }
            QuizzUIHandler.instance.SetHealthUI(health);
        }
    }
}