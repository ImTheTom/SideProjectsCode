using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour {
    private float screenHeight = 0f;
    private float screenWidth = 0f;
    private float halfScreenHeight = 0f;
    private float halfScreenWidth = 0f;
    // Use this for initialization
    void Start () {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        halfScreenHeight = screenHeight / 2;
        halfScreenWidth = screenWidth / 2;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI() {
        if (GUI.Button(new Rect(halfScreenWidth - 50, halfScreenHeight-30, 100, 20), "New Game")) {
            SceneManager.LoadScene("Main");
            PlayerPrefs.SetInt("LevelOn", 0);
        } else if (GUI.Button(new Rect(halfScreenWidth - 50, halfScreenHeight, 100, 20), "Level Select")) {
             SceneManager.LoadScene("LevelSelect");
        } else if (GUI.Button(new Rect(halfScreenWidth - 50, halfScreenHeight+30, 100, 20), "Exit Game")) {
            Application.Quit();
        }
    }
}
