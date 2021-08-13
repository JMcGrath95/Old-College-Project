using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;

    private PlayerHealth playerHealth;

    private void Awake()
    {       
        GameController.GameStarted += OnGameStarted;
    }

    private void OnGameStarted()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.DeathEvent += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        gameOverScreen.SetActive(true);
        playerHealth.DeathEvent -= OnPlayerDeath;
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("Main Menu"); 
        Time.timeScale = 1;
    }

    public void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene("Game Scene"); 
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        GameController.GameStarted -= OnGameStarted;
    }
}
