using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public CameraMovement cam;

    public void StartGame()
    {
        Player.GetComponent<PlayerHealth>().DeathEvent += GameController_DeathEvent;
        NavMeshBuilder.BuildNavMesh();
        Instantiate(Player, new Vector3(0, 0, 0), Quaternion.identity);
        cam.FindPlayer();
    }

    private void GameController_DeathEvent()
    {
        GameOver();
    }

    public void GameOver()
    {
        Application.Quit();
    }
}
