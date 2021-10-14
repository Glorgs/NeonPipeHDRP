using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGamePlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject LifeBar;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [SerializeField] private RawImage[] hearts;

    public int blinkFrame = 3;

    private int score = 0;
    private CanvasRenderer scoreRenderer;

    private void Start() {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        scoreRenderer = scoreText.gameObject.GetComponent<CanvasRenderer>();
    }

    public void UpdateScoreText(int currentScore) {
        score = currentScore;
        scoreText.SetText(currentScore.ToString());
    }

    public int GetScore() {
        return score;
    }

    public void UpdateLife(int currentLife) {
        Debug.Log("UpdateLife");
        for (int i = 0; i < hearts.Length; i++) {
            if (i < currentLife) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }
}
