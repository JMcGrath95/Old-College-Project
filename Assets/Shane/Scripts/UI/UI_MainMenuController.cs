using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Deals with switching between panels in main menu;
public class UI_MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;

    private GameObject activePanel;

    private void Start()
    {
        activePanel = mainMenuPanel;
    }

    private void DisableCurrentPanel() => activePanel.SetActive(false);

    public void EnableSettingsPanel()
    {
        DisableCurrentPanel();

        activePanel = settingsPanel;
        settingsPanel.SetActive(true);
    }

    public void EnableMainMenuPanel()
    {
        DisableCurrentPanel();

        activePanel = mainMenuPanel;
        mainMenuPanel.SetActive(true);
    }

}
