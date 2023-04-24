using ObjectPool;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BombController : MonoBehaviour
{
    [HideInInspector] public PoolSpawner poolSpawner;
    [SerializeField] DisarmTheBombData bombData;
    private Button bombButton;
    private float timerToExplode = 0f;

    private void Start()
    {
        bombButton = GetComponent<Button>();
        bombButton.onClick.AddListener(()=> DisarmBomb());
    }

    private void DisarmBomb()
    {
        poolSpawner.ReturnToPool("Bomb", gameObject);
        // Increase the time
    }

    private void Update()
    {
        timerToExplode += Time.deltaTime;
        if (timerToExplode >= bombData.timeToExplode)
        {
            timerToExplode = 0f;
            poolSpawner.ReturnToPool("Bomb", gameObject);
            LifeHandler.instance.LoseHealth();
        }
    }
}
