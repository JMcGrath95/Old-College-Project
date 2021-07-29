using TMPro;
using UnityEngine;

public class UI_ScoreIncrease : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI txtScoreIncrease;
    [Header("Time for text to dissapear")]
    [SerializeField] private float timeToDissapear;

    //Amount to increase.
    private int increaseValue;
    //Timer to dissapear.
    private Timer dissapearTimer;

    private UI_Score scoreDisplay;


    private void Awake()
    {
        scoreDisplay = FindObjectOfType<UI_Score>();
        scoreDisplay.IncreaseDisplayEndedEvent += OnScoreDisplayStoppedIncreasing;
        PlayerScoreController.ScoreIncreasedEvent += OnPlayerScoreIncreased;
    }

    //Display increase text when player score increases.
    private void OnPlayerScoreIncreased(int increase)
    {
        if (dissapearTimer != null)
            dissapearTimer.Reset();

        increaseValue += increase;
        txtScoreIncrease.text = increaseValue.ToString();
    }

    //Start countdown to increase text dissapearing.
    private void OnScoreDisplayStoppedIncreasing()
    {
        dissapearTimer = new Timer(this, timeToDissapear, ResetDisplay); 
        dissapearTimer.Start();
    }

    //Hide text.
    private void ResetDisplay()
    {
        increaseValue = 0;
        txtScoreIncrease.text = string.Empty;

        dissapearTimer = null;
    }

    private void OnDestroy()
    {
        scoreDisplay.IncreaseDisplayEndedEvent -= OnScoreDisplayStoppedIncreasing;
        PlayerScoreController.ScoreIncreasedEvent -= OnPlayerScoreIncreased;
    }
}