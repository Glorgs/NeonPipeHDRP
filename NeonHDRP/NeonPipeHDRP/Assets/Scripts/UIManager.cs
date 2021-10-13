using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void Quit() {
        Application.Quit();
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
    }

    public void RestartGame() {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void GoToMenu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
