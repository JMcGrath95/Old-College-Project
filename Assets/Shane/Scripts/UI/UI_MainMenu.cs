using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    public void OnQuitButtonClicked() => Application.Quit(0);
    public void OnPlayButtonClicked() => SceneManager.LoadScene("Game Scene");

}
