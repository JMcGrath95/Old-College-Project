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
        Instantiate(Player, new Vector3(0, 0, 0), Quaternion.identity);
        NavMeshBuilder.BuildNavMesh();
        cam.FindPlayer();
    }

    public void GameOver()
    {
        Application.Quit();
    }
}