using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int maxHealth;
    public float initialTime;
}
