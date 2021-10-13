using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGamePlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject LifeBar;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [SerializeField] private RawImage[] hearts;

    private void Start() {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        
    }

    public void UpdateScoreText(int currentScore) {
        scoreText.SetText(currentScore.ToString());
    }

    public void UpdateLife(int currentLife) {
        for (int i = 0; i < hearts.Length; i++) {
            if (i < currentLife) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = true;
            }
        }
    }
}
