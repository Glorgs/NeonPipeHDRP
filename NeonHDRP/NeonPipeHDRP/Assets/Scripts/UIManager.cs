using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class UIManager : MySingleton<UIManager>
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject EndMenu;
    [SerializeField] private TextMeshProUGUI winner;
    private PlayerInputAction playerAction;
    private InputActionMap actionMap;

    private void Awake() {
        playerAction = new PlayerInputAction();
        actionMap = playerAction.asset.FindActionMap("Player1");

        actionMap["Pause"].performed += ctx => PauseGame();
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void Quit() {
        Application.Quit();
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }

    public void RestartGame() {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void GoToMenu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void ShowEndMenu() {
        Time.timeScale = 0f;
        EndMenu.SetActive(true);

        //Display correct player
    }
}
