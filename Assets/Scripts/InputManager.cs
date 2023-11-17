using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputActions InputActions { get; set; }

    [Tooltip("Левое оружие.")][SerializeField]
    private Gun leftGun;
    [Tooltip("Правое оружие.")][SerializeField]
    private Gun rightGun;

    private void Awake()
    {
        InputActions = new InputActions();
        InputActions.GunsActions.LeftFire.performed += LeftFire;
        InputActions.GunsActions.RightFire.performed += RightFire;
    }

    private void RightFire(InputAction.CallbackContext obj)
    {
        rightGun.Fire();
    }

    private void LeftFire(InputAction.CallbackContext obj)
    {
        leftGun.Fire();
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    private void OnDestroy()
    {
        InputActions.GunsActions.LeftFire.performed -= LeftFire;
        InputActions.GunsActions.RightFire.performed -= RightFire;
    }
}