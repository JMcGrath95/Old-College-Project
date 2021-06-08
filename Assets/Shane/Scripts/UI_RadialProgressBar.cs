using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RadialProgressBar : MonoBehaviour
{
    private Image image;

    public event Action ProgressBarFilledEvent;


    [SerializeField] private float secondsUntilFull;
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
