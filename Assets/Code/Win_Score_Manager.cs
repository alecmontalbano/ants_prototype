using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Win_Score_Manager : MonoBehaviour
{

    private int scoreValue;
    private TextMeshProUGUI scoreUI;

    void Start()
    {
        scoreUI = GetComponentInChildren<TextMeshProUGUI>();
        scoreValue = PlayerPrefs.GetInt( "Player Score" );
    }

    void Update()
    {
        if (scoreUI == null)
        {
            scoreUI = GetComponentInChildren<TextMeshProUGUI>();
        }

        string scoreValueStr = scoreValue.ToString();
        scoreUI.text = scoreValueStr;
    }
}
