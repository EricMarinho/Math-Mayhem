using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Operator
{
    Plus,
    Minus,
    Multiply,
    Divide
}

public enum Condition
{
    None,
    GreaterThan,
    LessThan,
    EqualTo,
    GreaterThanOrEqualTo,
    LessThanOrEqualTo,
    NotEqualTo
}

[Serializable]
public class Answer
{
    public int value;
    public string text;
}

[Serializable]
public class MinMax
{
    public int minValue;
    public int maxValue;
}

[Serializable]
public class Question
{
    public bool isRandomQuestion = true;

    [Header("Random Question Options\n")]
    public List<MinMax> operationNumbers;
    public Operator operationOperator;
    public Condition operationCondition = Condition.None;
    public int answerMarginError = 2;
    public int answersQuantity = 3;

    [Header("\n\nCustom Question Options\n")]
    public string sentence;
    public int correctAnswer;
    public List<Answer> possibleAnswers;
}