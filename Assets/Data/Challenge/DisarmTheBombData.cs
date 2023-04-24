using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DisarmTheBombData", menuName = "Data/Challenge/DisarmTheBombData")]
public class DisarmTheBombData : ScriptableObject
{
    public float timeToExplode;
    public MinMax bombSpawnTime;
    public float bombTimeToGain;
}
