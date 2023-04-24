using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizzData", menuName = "Data/Quizz/QuizzData")]
public class QuizzData : ScriptableObject
{
    public List<Question> questions;
}
