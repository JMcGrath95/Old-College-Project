using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

//A progress bar that fills from 0 - 1.
//Pass in how many seconds until progress bar reaches full amount.

public class UI_ProgressBar : MonoBehaviour
{
    private Image image;

    public event Action ProgressBarFilledEvent;


    private void Awake() => image = GetComponent<Image>();

    public void ResetProgressBar() => image.fillAmount = 0;

    public void StartFillingProgressBar(float secondsUntilFull) => StartCoroutine(FillProgressBarCoroutine(secondsUntilFull)); 
    private IEnumerator FillProgressBarCoroutine(float secondsUntilFull)
    {
        Timer timer = new Timer(this,timeToStop: secondsUntilFull,callbacksOnTimerEnd: ()=> ProgressBarFilledEvent?.Invoke());
        timer.Start();

        while(!timer.ended)
        {
            image.fillAmount = timer.elapsed / secondsUntilFull;
            yield return null;
        }
    }
}
