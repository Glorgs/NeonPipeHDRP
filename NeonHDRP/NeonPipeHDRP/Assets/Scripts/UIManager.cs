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
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private TextMeshProUGUI winner;
    private PlayerInputAction playerAction;
    private InputActionMap actionMap;
    private bool isPaused;

    private void Awake() {
        playerAction = new PlayerInputAction();
        actionMap = playerAction.asset.FindActionMap("Player1");

        actionMap["Pause"].performed += ctx => EscapeButton();
        isPaused = false;
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void Quit() {
        Application.Quit();
    }

    private void EscapeButton() {
        if (isPaused) ResumeGame();
        if (!isPaused) PauseGame();
    }

    public void PauseGame() {
        isPaused = true;
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
    }

    public void ResumeGame() {
        isPaused = false;
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
        InGameUI.SetActive(false);
        EndMenu.SetActive(true);
        Time.timeScale = 0f;
        //Display correct player
    }

    private void OnEnable() {
        playerAction.Enable();
    }

    private void OnDisable() {
        playerAction.Disable();
    }
}
