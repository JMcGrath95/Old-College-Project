using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    //Maybe have buttons here as well, with events to fire when they are pressed.

    //Movement.
    public static Vector3 MovementInput { get; private set; }
    public static bool IsMovementInput { get { return MovementInput != Vector3.zero; } }
    private Camera mainCamera; 
    [SerializeField] private FixedJoystick fixedJoystick;

    //Attacking.
    [SerializeField] private Button btnAttack;
    public static event Action AttackInputEvent;

    private void Start()
    {
        mainCamera = Camera.main;

#if UNITY_ANDROID || UNITY_IOS
        btnAttack.onClick.AddListener(OnAttackButtonClick);
       
#endif
    }

    private void OnAttackInput() => AttackInputEvent?.Invoke();

    private void Update()
    {
        #region Update Movement
#if UNITY_ANDROID || UNITY_IOS
        MovementInput = new Vector3(fixedJoystick.Horizontal, 0, fixedJoystick.Vertical);

#elif UNITY_STANDALONE
        MovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
#endif

        MovementInput = mainCamera.transform.TransformDirection(MovementInput) * Time.deltaTime;
        MovementInput = new Vector3(MovementInput.x, 0, MovementInput.z).normalized;
        #endregion

        #region Update Attack

#if UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Mouse0))
            OnAttackInput();
#endif

        #endregion

    }

}
