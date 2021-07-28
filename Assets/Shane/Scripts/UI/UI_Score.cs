using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    public static event Action IncreaseDisplayEndedEvent;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private TextMeshProUGUI txtScoreIncrease;

    [Header("Score")]
    [SerializeField] private float score;
    [SerializeField] private float increaseSpeed;
    [SerializeField] private float timeToDissapearScoreIncreaseText;
    private int scoreTarget;
    //private int scoreIncrease = 0;

    private bool IncreaseScoreDisplay;

    private float elapsedSecondsAtScoreDisplayReachingTarget;

    private Coroutine IncreaseScoreCoroutine = null;

    private void Awake()
    {
        UI_ScoreController.TempScoreIncreaseEvent += OnPlayerScoreIncreased;
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

    //public void IncreaseScore(int amount)
    //{
    //    ScoreDisplayIncreasing = true;

    //    scoreTarget += amount;
    //    //scoreIncrease += amount;

    //    //txtScoreIncrease.text = scoreIncrease.ToString();

    //    if(IncreaseScoreCoroutine == null)
    //    {
    //        IncreaseScoreCoroutine = StartCoroutine(enu_IncreaseScoreDisplay());
    //    }
    //}

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



    //private IEnumerator enu_IncreaseScoreDisplay()
    //{
    //    while (true)
    //    {
    //        //Increase score continuously.
    //        if(score < scoreTarget)
    //        {
    //            score += Time.deltaTime * increaseSpeed;
    //            txtScore.text = score.ToString("F0");
    //        }
    //        else //Reached score.
    //        {
    //            if(ScoreDisplayIncreasing)
    //            {
    //                //Set incase increase was really fast and goes over.
    //                score = scoreTarget;
    //                txtScore.text = scoreTarget.ToString("F0");

    //                //Grab time.
    //                elapsedSecondsAtScoreDisplayReachingTarget = Time.time;

    //                ScoreDisplayIncreasing = false;
    //            }

    //            //End coroutine.
    //            if(Time.time >= elapsedSecondsAtScoreDisplayReachingTarget + timeToDissapearScoreIncreaseText)
    //            {
    //                scoreIncrease = 0;
    //                txtScoreIncrease.text = string.Empty;

    //                IncreaseScoreCoroutine = null;
    //                break;
    //            }
    //        }

    //        yield return null;
    //    }



        //while(score <= scoreTarget)
        //{
        //    score += Time.deltaTime * increaseSpeed;
        //    txtScore.text = score.ToString("F0");
        //    yield return null;
        //}

        ////Set incase increase was really fast and goes over.
        //score = scoreTarget;
        //txtScore.text = scoreTarget.ToString("F0");

        ////Hide score increase text after x seconds.
        //yield return new WaitForSeconds(timeToDissapearScoreIncreaseText);

        //scoreIncrease = 0;
        //txtScoreIncrease.text = string.Empty;

    //    //IncreaseScoreCoroutine = null;
    //}
}
