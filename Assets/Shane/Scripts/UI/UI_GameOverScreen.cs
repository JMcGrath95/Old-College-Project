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
        
        GameController.GameStarted += GameController_GameStarted;
    }

    private void GameController_GameStarted()
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
        SceneManager.LoadScene(6); 
        Time.timeScale = 1;
    }

    public void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene(1); 
        Time.timeScale = 1;
    }
}
