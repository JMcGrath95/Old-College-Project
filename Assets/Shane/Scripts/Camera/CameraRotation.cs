using System.Diagnostics;
using UnityEngine;

//Rotates player around player on certain input (probably q and e).
public class CameraRotation : MonoBehaviour
{
    private Transform player;

    [SerializeField] private bool EnableRotationFromStart;

    [Header("Speed To Rotate")]
    [SerializeField] private float rotationSpeed;

    //Would have used getaxis for this but wasn't able to change axis settings under "Project Settings - Input Manager" to work with keybinds manager.
    private float rotationInput
    {   
        get 
        {
            if (Input.GetKey(KeyBindsManager.keyBinds["Camera Rotate Right"]))
            {
                return -1;
            }
            else if (Input.GetKey(KeyBindsManager.keyBinds["Camera Rotate Left"]))
            {
                return 1;
            }
            else
                return 0;         
        } 
    }

    private void Start()
    {
        if (EnableRotationFromStart)
            FindPlayer();
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null || PlayerStateMachine.PlayerControlState != PlayerControlState.InControl)
            return;

        transform.RotateAround(player.position,Vector3.up,rotationInput * rotationSpeed);
    }
}
