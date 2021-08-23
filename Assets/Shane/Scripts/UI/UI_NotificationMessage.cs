using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_NotificationMessage : MonoBehaviour
{
    [Header("Components")]
    private TextMeshProUGUI myTextMeshProComponent;

    [Header("Move Settings")]
    [SerializeField] private float moveSpeed;
    [Header("Fading Alpha Settings")]
    [SerializeField] private float alphaTarget;
    [SerializeField] private float timeToFadeToAlphaTarget;

    private void Awake()
    {
        myTextMeshProComponent = GetComponent<TextMeshProUGUI>();
    }

    public void SetMessage(string message) => myTextMeshProComponent.text = message;

    public void MoveToTargetAndFade(Vector3 targetPos)
    {
        StartCoroutine(MoveToTargetAndFadeCoroutine(targetPos));
    }

    private IEnumerator MoveToTargetAndFadeCoroutine(Vector3 targetPos)
    {
        myTextMeshProComponent.CrossFadeAlpha(alphaTarget, timeToFadeToAlphaTarget, true);
        Vector3 startingPos = transform.position;

        float i = 0;

        while(i <= 1)
        {
            i += Time.deltaTime * moveSpeed;
            myTextMeshProComponent.transform.position = Vector3.Lerp(startingPos, targetPos, i);
            yield return null;
        }

        Destroy(this.gameObject);
    }
}