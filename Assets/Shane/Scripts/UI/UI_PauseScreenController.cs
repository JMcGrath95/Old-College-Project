using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PauseScreenController : MonoBehaviour
{
    private Action escapeInputDelegate;

    [SerializeField] private GameObject mainPauseScreen;
    [SerializeField] private GameObject optionsPauseScreen;
    [SerializeField] private GameObject quitConfirmationScreen;
    private GameObject currentPauseScreen;

    private void Start()
    {
        escapeInputDelegate = EnableMainPauseScreen;
    }

    public void EnableMainPauseScreen()
    {
        EnableScreen(mainPauseScreen);

        escapeInputDelegate = ResumeGame;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        currentPauseScreen = null;
        escapeInputDelegate = EnableMainPauseScreen;
        mainPauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void EnableOptionsScreen()
    {
        EnableScreen(optionsPauseScreen);

        escapeInputDelegate = EnableMainPauseScreen;
    }

    public void EnableQuitConfirmationScreen()
    {
        EnableScreen(quitConfirmationScreen);

        escapeInputDelegate = EnableMainPauseScreen;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0); //Main menu scene. 0 isn't main menu scene at the moment.
    }

    public void SaveSettings()
    {
        //Save volume setting as well??

        KeyBindsManager.SaveKeybindsToJSON();

        EnableMainPauseScreen();  
    }

    private void DisableCurrentPauseScreen()
    {
        if(currentPauseScreen != null)
            currentPauseScreen.SetActive(false);
    }
    private void EnableScreen(GameObject screenToEnable)
    {
        DisableCurrentPauseScreen();
        screenToEnable.SetActive(true);
        currentPauseScreen = screenToEnable;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            escapeInputDelegate();
        }
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }
}
