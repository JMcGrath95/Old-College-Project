using UnityEngine;

//Rotates player around player on certain input (probably q and e).
public class CameraRotation : MonoBehaviour
{
    private Transform player;

    [Header("Speed To Rotate")]
    [SerializeField] private float rotationSpeed;

    private void Start() => player = GameObject.FindGameObjectWithTag("Player").transform;

    void Update()
    {
        transform.RotateAround(player.position,Vector3.up,Input.GetAxisRaw("Camera Rotation") * rotationSpeed);
    }
}
