using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText; // The question itself
    public string[] options;     // Array for answer options
    public int correctOptionIndex; // Index of the correct option
}

[CreateAssetMenu(fileName = "NewQuestionList", menuName = "Questions/Questioning")]
public class Questionaing : ScriptableObject
{
    public Question[] questions; // Array of questions
}

