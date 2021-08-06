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
        SceneManager.LoadScene(6); //Choose correct scene later.
        Time.timeScale = 1;
    }

    public void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene(5); //Choose correct scene later.
        Time.timeScale = 1;
    }
}
