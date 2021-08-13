using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public CameraFollowPlayer cam;

    public static event Action GameStarted;

    public void StartGame()
    {
        Instantiate(Player, new Vector3(0, 0, 0), Quaternion.identity);
        cam.FindPlayer();
        GameStarted();
    }

    public void GameOver()
    {
        Application.Quit();
    }
}
