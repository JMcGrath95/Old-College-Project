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
    [SerializeField] private KeyCode keyAttack;
    [SerializeField] private Button btnAttack;
    public static event Action AttackInputEvent;

    //Dashing. Cooldown here?
    [SerializeField] private KeyCode keyDash;
    [SerializeField] private Button btnDash;
    public static event Action DashInputEvent;

    private void Start()
    {
        mainCamera = Camera.main;

        #if UNITY_ANDROID || UNITY_IOS
        btnAttack.onClick.AddListener(OnAttackInput);
        btnDash.onClick.AddListener(OnDashInput);
        #endif
    }

    private void OnAttackInput() => AttackInputEvent?.Invoke();
    private void OnDashInput()
    {
        if(IsMovementInput)
            DashInputEvent?.Invoke();
    }

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
        if (Input.GetKeyDown(keyAttack))
            OnAttackInput();
        #endif

        #endregion

        #region Update Dash

        #if UNITY_STANDALONE
        if (Input.GetKeyDown(keyDash))
            OnDashInput();
        #endif

        #endregion

    }



    private void OnDestroy()
    {
        //Unsub events if on mobile.
        #if UNITY_ANDROID || UNITY_IOS

        btnAttack.onClick.RemoveListener(OnAttackInput);
        btnDash.onClick.RemoveListener(OnDashInput);

        #endif
    }

}
