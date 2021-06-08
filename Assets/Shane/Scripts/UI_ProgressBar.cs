using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//A progress bar that fills from 0 - 1.
//Pass in how many seconds until progress bar reaches full amount.

public class UI_ProgressBar : MonoBehaviour
{
    private Image image;

    public event Action ProgressBarFilledEvent;

    public bool IsActive { get { return image.fillAmount >= 1f; } }

    private void Awake() => image = GetComponent<Image>();

    public void ResetProgressBar() => image.fillAmount = 0;

    public void StartFillingProgressBar(float seconds) => StartCoroutine(FillProgressBarCoroutine(seconds)); 
    private IEnumerator FillProgressBarCoroutine(float seconds)
    {
        float timer = 0f;

        while(timer <= seconds)
        {
            timer += Time.deltaTime;
            image.fillAmount = timer / seconds;
            yield return null;
        }

        ProgressBarFilledEvent?.Invoke();

    }
}
