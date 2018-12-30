using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorScript : MonoBehaviour {
    private float screenHeight = 0f;
    private float screenWidth = 0f;
    private float halfScreenHeight = 0f;
    private float halfScreenWidth = 0f;
    // Use this for initialization
    void Start() {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        halfScreenHeight = screenHeight / 2;
        halfScreenWidth = screenWidth / 2;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnGUI() {
        if (GUI.Button(new Rect(halfScreenWidth - 50, halfScreenHeight - 90, 100, 20), "Level 1")) {
            SceneManager.LoadScene("Main");
            PlayerPrefs.SetInt("LevelOn", 0);
        } else if (GUI.Button(new Rect(halfScreenWidth - 50, halfScreenHeight - 60, 100, 20), "Level 2")) {
            PlayerPrefs.SetInt("LevelOn", 1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("LevelOn").ToString());
        } else if (GUI.Button(new Rect(halfScreenWidth - 50, halfScreenHeight - 30, 100, 20), "Level 3")) {
            PlayerPrefs.SetInt("LevelOn", 2);
            SceneManager.LoadScene(PlayerPrefs.GetInt("LevelOn").ToString());
        } else if (GUI.Button(new Rect(halfScreenWidth - 50, halfScreenHeight, 100, 20), "Level 4")) {
            PlayerPrefs.SetInt("LevelOn", 3);
            SceneManager.LoadScene(PlayerPrefs.GetInt("LevelOn").ToString());
        } else if (GUI.Button(new Rect(halfScreenWidth - 50, halfScreenHeight +30, 100, 20), "Level 5")) {
            PlayerPrefs.SetInt("LevelOn", 4);
            SceneManager.LoadScene(PlayerPrefs.GetInt("LevelOn").ToString());
        } else if (GUI.Button(new Rect(halfScreenWidth - 50, halfScreenHeight + 60, 100, 20), "Back")) {
            SceneManager.LoadScene("Start");
        } else if (GUI.Button(new Rect(halfScreenWidth - 50, halfScreenHeight + 90, 100, 20), "Exit Game")) {
            Application.Quit();
        }
    }
}
