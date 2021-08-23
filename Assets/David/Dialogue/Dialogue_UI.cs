using RPG.Dialogue;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    

    public class Dialogue_UI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] Button nextButton;
        [SerializeField] GameObject AIResponse;
        [SerializeField] Transform choiceRoute;
        [SerializeField] GameObject choicePrefab;

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            nextButton.onClick.AddListener(Next);

            UpdateUI();
        }


        void Next()
        {
            playerConversant.Next();
            UpdateUI();
        }


        void UpdateUI()
        {
            
            AIResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoute.gameObject.SetActive(playerConversant.IsChoosing());
            if(playerConversant.IsChoosing())
            {
                foreach (Transform item in choiceRoute)
                {
                    Destroy(item.gameObject);
                }
                foreach (string choiceText in playerConversant.GetChoices())
                {
                    GameObject choiceInstance = Instantiate(choicePrefab, choiceRoute);
                    var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                    textComp.text = choiceText;
                }
            }
            else
            {
                AIText.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }
            
        }
    }
}
