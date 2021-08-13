using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PauseScreenController : MonoBehaviour
{
    public static event Action GamePausedEvent;
    public static event Action GameUnpausedEvent;

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
        GamePausedEvent?.Invoke();
    }

    public void ResumeGame()
    {
        currentPauseScreen = null;
        escapeInputDelegate = EnableMainPauseScreen;
        mainPauseScreen.SetActive(false);
        Time.timeScale = 1;
        GameUnpausedEvent?.Invoke();
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
        SceneManager.LoadScene("Main Menu"); 
        Time.timeScale = 1;
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
