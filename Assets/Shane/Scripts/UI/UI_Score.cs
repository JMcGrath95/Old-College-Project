using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    public event Action IncreaseDisplayEndedEvent;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private TextMeshProUGUI txtScoreIncrease;

    [Header("Score")]
    [SerializeField] private float score;
    [SerializeField] private float increaseSpeed;
    [SerializeField] private float timeToDissapearScoreIncreaseText;
    private int scoreTarget;

    private bool IncreaseScoreDisplay;

    private void Awake()
    {
        PlayerScoreController.ScoreIncreasedEvent += OnPlayerScoreIncreased;
    }

    private void OnPlayerScoreIncreased(int increase)
    {
        scoreTarget += increase;
        IncreaseScoreDisplay = true;
    }

    private void Start()
    {
        scoreTarget = (int)score;
    }

    private void Update()
    {
        if (!IncreaseScoreDisplay)
            return;

        //Increase score display until it reaches players score.
        if (score < scoreTarget)
        {
            score += Time.deltaTime * increaseSpeed;
            txtScore.text = score.ToString("F0");
        }
        else //Reached score.
        {
            IncreaseDisplayEndedEvent?.Invoke();

            score = scoreTarget;
            txtScore.text = scoreTarget.ToString("F0");

            IncreaseScoreDisplay = false;
        }
    }
}