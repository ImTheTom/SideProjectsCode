using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour {
    private bool gameover;
    private float screenHeight = 0f;
    private float screenWidth = 0f;
    private float halfScreenHeight = 0f;
    private float halfScreenWidth = 0f;
    private bool gameWon = false;
    private GameObject ball;
    private int levelOn;

    // Use this for initialization
    void Start () {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        halfScreenHeight = screenHeight / 2;
        halfScreenWidth = screenWidth / 2;
        gameover = false;
        ball = GameObject.Find("Ball");
    }
	
	// Update is called once per frame
	void Update () {
		if(ball == null) {
            gameover = true;
        }
	}

    private void OnGUI() {
        if (gameover && gameWon) {
            if (GUI.Button(new Rect(halfScreenWidth - 160, halfScreenHeight, 100, 20), "Next Level")) {
                PlayerPrefs.SetInt("LevelOn", PlayerPrefs.GetInt("LevelOn") + 1);
                SceneManager.LoadScene(PlayerPrefs.GetInt("LevelOn").ToString());
            } else if (GUI.Button(new Rect(halfScreenWidth - 50, halfScreenHeight, 100, 20), "Retry Level")) {
                if (0 == PlayerPrefs.GetInt("LevelOn")) {
                    SceneManager.LoadScene("Main");
                } else {
                    SceneManager.LoadScene(PlayerPrefs.GetInt("LevelOn").ToString());
                }
            } else if (GUI.Button(new Rect(halfScreenWidth + 60, halfScreenHeight, 100, 20), "Exit To Menu")) {
                SceneManager.LoadScene("Start");
            }
        }else if(gameover && !gameWon) {
            if (GUI.Button(new Rect(halfScreenWidth - 50, halfScreenHeight, 100, 20), "Retry Level")) {
                if (0 == PlayerPrefs.GetInt("LevelOn")) {
                    SceneManager.LoadScene("Main");
                } else {
                    try {
                        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelOn").ToString());
                    } catch {
                        SceneManager.LoadScene("Start");
                    }
                }
            } else if (GUI.Button(new Rect(halfScreenWidth +60, halfScreenHeight, 100, 20), "Exit To Menu")) {
                SceneManager.LoadScene("Start");
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Ball") {
            Destroy(other.gameObject);
            gameover = true;
            gameWon = true;
        }
    }

    public void BallStopped() {
        gameover=true;
    }
}
