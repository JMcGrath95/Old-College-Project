using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ScoreIncrease : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI txtScoreIncrease;

    [SerializeField] private float timeToDissapear;

    private int increaseValue;
    private Timer dissapearTimer;

    // Needs to be.... 

    // Notified of score increase. 
    // Notified of when score display stops increasing - so it can start countdown to when it should dissapear.

    private void Awake()
    {
        UI_ScoreController.TempScoreIncreaseEvent += OnPlayerScoreIncreased;
        UI_Score.IncreaseDisplayEndedEvent += OnScoreDisplayStoppedIncreasing;
    }

    private void OnScoreDisplayStoppedIncreasing()
    {
        dissapearTimer = new Timer(this, timeToDissapear, ResetDisplay);
        dissapearTimer.Start();
    }

    private void OnPlayerScoreIncreased(int increase)
    {
        if (dissapearTimer != null)
            dissapearTimer.Reset();


        increaseValue += increase;
        txtScoreIncrease.text = increaseValue.ToString();
    }

    private void ResetDisplay()
    {
        increaseValue = 0;
        txtScoreIncrease.text = string.Empty;

        dissapearTimer = null;
    }

    private void OnDestroy()
    {
        UI_ScoreController.TempScoreIncreaseEvent -= OnPlayerScoreIncreased;        
    }
}
