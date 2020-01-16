using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Manager : MonoBehaviour
{
    public int scoreValue;
    private TextMeshProUGUI scoreUI;

    void Start()
    {
        scoreUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if ( scoreUI == null )
        {
            scoreUI = GetComponentInChildren<TextMeshProUGUI>();
        }

        string scoreValueStr = scoreValue.ToString();
        scoreUI.text = scoreValueStr;
    }
}
