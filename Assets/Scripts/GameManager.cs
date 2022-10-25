using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public enum Scene {
        MainMenu,
        FirstLevel,
        GameOver
    }

    public Scene currentScene { get; private set; } = Scene.MainMenu;

    public enum GameStatus {
        NotStarted,
        Active,
        Paused
    }

    public GameStatus CurrentGameStatus { get; private set; } = GameStatus.NotStarted;

    public Menu menu;

    private void Awake() {
        if (instance) {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(Scene scene) {
        currentScene = scene;

        string sceneName;

        switch (scene) {
            case Scene.FirstLevel: {
                menu.canvas.gameObject.SetActive(false);
                sceneName = "MainScene";
                break;
            }
            case Scene.GameOver: {
                menu.canvas.gameObject.SetActive(true);
                sceneName = "GameOverScene";
                break;
            }
            case Scene.MainMenu:
            default: {
                menu.canvas.gameObject.SetActive(true);
                sceneName = "MenuScene";
                break;
            }
        }

        Debug.Log("Loaded " + scene);
        SceneManager.LoadScene(sceneName);

        SetGameStatus();
        menu.SetStartName();
    }

    private void SetGameStatus() {
        switch (currentScene) {
            case Scene.MainMenu:
            case Scene.GameOver:
                CurrentGameStatus = GameStatus.NotStarted;
                break;
            default:
                CurrentGameStatus = GameStatus.Active;
                break;
        }
    }

    public void LoadNextScene() {
        LoadScene((int) currentScene == Enum.GetNames(typeof(Scene)).Length - 1 ? (currentScene = 0) : ++currentScene);

        if (currentScene != Scene.MainMenu || currentScene != Scene.GameOver) {
            CurrentGameStatus = GameStatus.Active;
        }
    }

    private void Update() {
        /*if (Input.GetKeyDown("space")) {
            CurrentGameStatus = GameStatus.Active;
        }*/
    }

    public void PauseGame() {
        CurrentGameStatus = GameStatus.Paused;
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        CurrentGameStatus = GameStatus.Active;
        Time.timeScale = 1;
    }

    public void OnPlayerDeath() {
        CurrentGameStatus = GameStatus.NotStarted;
        LoadScene(Scene.GameOver);
        // Invoke(nameof(OnPlayerDeathInternal), 5f);
    }

    private void OnPlayerDeathInternal() {
        // LoadScene(Scene.GameOver);
    }
}
