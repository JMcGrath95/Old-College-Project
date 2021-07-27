using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ScoreController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private UI_Score ui_Score;
    [SerializeField] private UI_ScoreIncrease ui_ScoreIncrease;


    private void Awake()
    {
        //On score increase subscription from event here.
    }


    //testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ui_Score.IncreaseScore(100);
        }
    }
}
