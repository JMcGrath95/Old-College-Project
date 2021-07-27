using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI txtScore;

    [Header("Score")]
    [SerializeField] private float score;
    [SerializeField] private float increaseSpeed;
    private int scoreTarget;

    private Coroutine IncreaseScoreCoroutine = null;


    private void Awake()
    {
        
    }

    private void Start()
    {
        scoreTarget = (int)score;
    }

    public void IncreaseScore(int amount)
    {
        scoreTarget += amount;

        if(IncreaseScoreCoroutine == null)
        {
            IncreaseScoreCoroutine = StartCoroutine(IncreaseScoreDisplay());
        }
    }

    private IEnumerator IncreaseScoreDisplay()
    {
        while(score <= scoreTarget)
        {
            score += Time.deltaTime * increaseSpeed;
            txtScore.text = score.ToString("F0");

            yield return null;
        }

        //Set incase increase was really fast and goes over.
        score = scoreTarget;
        txtScore.text = scoreTarget.ToString("F0");

        IncreaseScoreCoroutine = null;
    }
}
