using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Ensure you have this for using Button
//fetcing the questions and displaying 
public class QuestionDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI QuestionText; // Text for the question
    [SerializeField] private Questionaing QuestionData; // Reference to the ScriptableObject
    [SerializeField] private Button[] OptionButtons; // Array of buttons for options

    private int correctOptionIndex; // Store the index of the correct option
    private int currentQuestionIndex = 0; // Track the current question index
    private bool isAnswerCorrect = false; // To check if the answer is correct

    void Start()
    {
        DisplayQuestion(); // Display the first question when the game starts
    }

    // Method to display the current question based on currentQuestionIndex
    private void DisplayQuestion()
    {
        if (QuestionData != null && QuestionData.questions.Length > 0)
        {
            // Check if there are still questions to display
            if (currentQuestionIndex < QuestionData.questions.Length)
            {
                // Get the question based on the current question index
                Question selectedQuestion = QuestionData.questions[currentQuestionIndex];

                // Set the question text into the textMeshUPGUIPRo
                QuestionText.text = selectedQuestion.questionText;
                correctOptionIndex = selectedQuestion.correctOptionIndex; // Get the correct answer index

                // Display the options on the buttons
                for (int i = 0; i < OptionButtons.Length; i++)
                {
                    if (i < selectedQuestion.options.Length)
                    {
                        OptionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = selectedQuestion.options[i];
                        OptionButtons[i].gameObject.SetActive(true); // Show the button
                        int index = i; // Local copy for the closure
                        OptionButtons[i].onClick.RemoveAllListeners(); // Clear previous listeners
                        OptionButtons[i].onClick.AddListener(() => CheckAnswer(index)); // Add listener
                    }
                    else
                    {
                        OptionButtons[i].gameObject.SetActive(false); // Hide unused buttons
                    }
                }
            }
            else
            {
                // If all questions are answered, end the game
                Debug.LogWarning("All questions answered. Game over.");
                EndGame();
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            QuestionText.text = "No questions available.";
            Debug.LogWarning("Question data is not assigned or empty.");
        }
    }

    // Method to handle the answer check and move to the next question only if the answer is correct
    public void CheckAnswer(int selectedIndex)
    {
        // Check if the selected option is correct
        if (selectedIndex == correctOptionIndex)
        {
            Debug.Log("Correct Answer!");
            isAnswerCorrect = true;
        }
        else
        {
            Debug.Log("Incorrect Answer.");
            isAnswerCorrect = false;
        }

        // If the answer is correct, move to the next question
        if (isAnswerCorrect)
        {
            // Wait for a moment, then load the next question (using Invoke to delay)
            Invoke("NextQuestion", 1f); // 1 second delay before loading the next question
        }
        else
        {
            // If the answer is incorrect, you may want to show feedback and wait a bit before moving on
            Invoke("DisplayQuestion", 1f); // 1 second delay before retrying the same question
        }
    }

    // Method to move to the next question (if the answer is correct)
    private void NextQuestion()
    {
        // Increment the current question index to move to the next question
        currentQuestionIndex++;

        // Display the next question
        DisplayQuestion();
    }

    // Method to handle the end of the game (when all questions are answered)
    private void EndGame()
    {
        // Optionally: Hide buttons, show "Game Over" text, etc.
        Debug.Log("Game Over!");
        foreach (Button button in OptionButtons)
        {
            button.gameObject.SetActive(false); // Disable all buttons
        }
    }
}
