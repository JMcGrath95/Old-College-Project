using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
    public static event Action<int> ScoreChangedEvent;
    public static event Action<int> ScoreIncreasedEvent;

    private static int playerScore;

    public static int PlayerScore 
    { 
        get { return playerScore; }
        set 
        {
            int change = value - playerScore;

            //Score increased.
            if (change > 0)
                ScoreIncreasedEvent?.Invoke(change);


            playerScore = value;
            print($"Player score changed to {playerScore}");
            ScoreChangedEvent?.Invoke(playerScore);       
        } 
    }
}