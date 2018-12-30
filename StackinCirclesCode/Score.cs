using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private static int currentScore = 0;
    private static int highScore = 0;

    private static bool updateScore = false;
    private static bool newHighScore = false;
    private static bool gameOver = false;

    public Text currentScoreText;
    public Text currentHighscoreText;
    public Text gameOverText;

    public delegate void ScoreDelegate();

    public static ScoreDelegate RunEndGameFunctions;

    // This function sets up the text ui as well as the delegate
    void Start () {
        currentScore = 0;
        try {
            highScore = PlayerPrefs.GetInt("highscore");
        } finally {
            PlayerPrefs.SetInt("highscore", highScore);
        }
        currentHighscoreText.text = "Highscore:" + highScore.ToString();
        gameOverText.text = "";
        RunEndGameFunctions += UpdateGameOverVariable;
        RunEndGameFunctions += CheckNewHighscore;
    }
	
    // This function updates the text ui if need be
	void Update () {
        if (updateScore) {
            updateScore = false;
            currentScoreText.text = "Current Score:" + currentScore.ToString();
        }
        if (newHighScore) {
            newHighScore = false;
            currentHighscoreText.text = "Highscore:" + highScore.ToString();
        }
        if (gameOver) {
            gameOverText.text = "Game Over. Tap to start a new game.";
            gameOver = false;
        }
	}

    public static void UpdateCurrentScore() {
        currentScore += 1;
        updateScore = true;
    }

    // Checks if the current score is greater than the high score after the game is over.
    // Called through the RunEndGameFunctions
    public static void CheckNewHighscore() {
        if (currentScore > highScore) {
            newHighScore = true;
            UpdateHighscore();
        } else {
            newHighScore = false;
        }
    }

    public static void UpdateHighscore() {
        highScore = currentScore;
        PlayerPrefs.SetInt("highscore", highScore);
    }

    public static void UpdateGameOverVariable() {
        gameOver = true;
    }

    public int GetCurrentScore() {
        return currentScore;
    }
}
