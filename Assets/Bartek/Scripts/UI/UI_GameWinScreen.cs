using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameWinScreen : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
        PlayerStateMachine.PlayerControlState = PlayerControlState.Locked;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game Scene");
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
