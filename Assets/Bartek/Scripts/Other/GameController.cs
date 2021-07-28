using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public CameraFollowPlayer cam;

    public void StartGame()
    {
        Instantiate(Player, new Vector3(0, 0, 0), Quaternion.identity);
        cam.FindPlayer();
    }

    public void GameOver()
    {
        Application.Quit();
    }
}
