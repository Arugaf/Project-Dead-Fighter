using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour {
    public static Menu instance;
    
    public Canvas canvas;
    public Canvas about;

    public TextMeshProUGUI startButton;

    private void Awake() {
        if (instance) {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (Input.GetKeyDown("escape")) {
            OnEscButton();
        }
    }

    private void OnEscButton() {
        if (GameManager.instance.CurrentGameStatus == GameManager.GameStatus.Active) {
            canvas.gameObject.SetActive(true);
            GameManager.instance.PauseGame();
        }
        else if (GameManager.instance.CurrentGameStatus == GameManager.GameStatus.Paused) {
            canvas.gameObject.SetActive(false);
            about.gameObject.SetActive(false);
            GameManager.instance.ResumeGame();
        }
    }

    public void SetStartName() {
        switch (GameManager.instance.currentScene) {
            case GameManager.Scene.MainMenu: {
                startButton.SetText("Start game");
                break;
            }
            case GameManager.Scene.GameOver: {
                startButton.SetText("Restart game");
                break;
            }
            default: {
                startButton.SetText("Resume game");
                break;
            }
        }
    }

    public void OnStartClick() {
        switch (GameManager.instance.currentScene) {
            case GameManager.Scene.MainMenu: {
                GameManager.instance.LoadNextScene();
                break;
            }
            case GameManager.Scene.GameOver: {
                GameManager.instance.LoadScene(GameManager.Scene.FirstLevel);
                break;
            }
            default: {
                OnEscButton();
                break;
            }
        }
    }

    public void OnQuitClick() {
        Application.Quit();
    }

    public void OnAboutClick() {
        about.gameObject.SetActive(true);
        canvas.gameObject.SetActive(false);
    }

    public void OnBackClick() {
        about.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
    }
}
