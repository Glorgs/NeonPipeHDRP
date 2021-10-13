using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerText : MonoBehaviour
{
    private TextMeshProUGUI winnerText;
    public void Start() {
        winnerText = GetComponent<TextMeshProUGUI>();
        SetWinnerText(2);
    }

    public void SetWinnerText(int playerNb) {
        winnerText.SetText("Player" + playerNb);
        if (playerNb == 1) {
            winnerText.color = new Color(1.0f, 0.8f, 0f, 1.0f);
        } else {
            winnerText.color = new Color(0.8f, 1.0f, 0f, 1.0f);
        }
    }
}
