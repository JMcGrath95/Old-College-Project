using System;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    //Movement.
    public bool IsMovementInput { get { return MovementInput != Vector3.zero; } }
    public Vector3 MovementInput { get; private set; }
    private Camera mainCamera;

    [Header("Interacting")]
    [SerializeField] private string interactKeyBindName;
    [SerializeField] private Button btnInteract;
    private Func<KeyCode, bool> interactInputDelegate; //Not used yet. Need this if certain interaction areas you need to interact by holding button instead of single press.
    public bool InteractButtonPressed { get { return Input.GetKeyDown(KeyBindsManager.keyBinds[interactKeyBindName]);} }

    [Header("Attacking")]
    [SerializeField] private string attackKeyBindName;
    [SerializeField] private Button btnAttack;
    public bool AttackButtonPressed { get { return Input.GetKeyDown(KeyBindsManager.keyBinds[attackKeyBindName]);} }

    [Header("Dashing")]
    [SerializeField] private string dashKeyBindName;
    [SerializeField] private Button btnDash;
    public bool DashButtonPressed { get { return Input.GetKeyDown(KeyBindsManager.keyBinds[dashKeyBindName]);} }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        #region Check For Movement
#if UNITY_ANDROID || UNITY_IOS
        MovementInput = new Vector3(fixedJoystick.Horizontal, 0, fixedJoystick.Vertical);

#elif UNITY_STANDALONE
        MovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        #endif

        MovementInput = mainCamera.transform.TransformDirection(MovementInput) * Time.deltaTime;
        MovementInput = new Vector3(MovementInput.x, 0, MovementInput.z).normalized;
        #endregion
    }
}



//Old Code
//        #region Check For Attack

//#if UNITY_STANDALONE

//        if (Input.GetKeyDown(KeyBindsManager.keyBinds[attackKeyBindName]))
//            OnAttackInput();
//        //if (Input.GetKeyDown(keyAttack))
//        //    OnAttackInput();
//#endif

//        #endregion

//        #region Check For Dash

//#if UNITY_STANDALONE

//        if (Input.GetKeyDown(KeyBindsManager.keyBinds[dashKeyBindName]))
//            OnDashInput();
//        //if (Input.GetKeyDown(keyDash))
//        //    OnDashInput();
//#endif

//        #endregion

//        #region Check For Interaction


//        //if (Input.GetKeyDown(keyInteract))
//        //    OnInteractInput();

//        if (Input.GetKeyDown(KeyBindsManager.keyBinds[interactKeyBindName]))
//            OnInteractInput();

//        #endregion

//Old Input raising methods
//Raising input events.
//private void OnInteractInput() => InteractInputEvent?.Invoke();
//private void OnAttackInput() => AttackInputEvent?.Invoke();
//private void OnDashInput()
//{
//    if (IsMovementInput)
//        DashInputEvent?.Invoke();
//}
