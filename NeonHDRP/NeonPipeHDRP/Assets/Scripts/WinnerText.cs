using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI player1Score;
    [SerializeField] private TextMeshProUGUI player2Score;
    public void Start() {
        winnerText = GetComponent<TextMeshProUGUI>();
    }

    public void SetWinnerText(int p1Score, int p2Score) {
        int playerNb;
        if (p1Score > p2Score) { playerNb = 1; } else { playerNb = 2; }

        winnerText.SetText("Player" + playerNb);
        if (playerNb == 1) {
            winnerText.color = new Color(0.9f, 0f, 0.9f, 1.0f);
        } else {
            winnerText.color = new Color(0.3f, 0.9f, 0f, 1.0f);
        }

        player1Score.SetText(p1Score.ToString());
        player2Score.SetText(p2Score.ToString());
    }
}
