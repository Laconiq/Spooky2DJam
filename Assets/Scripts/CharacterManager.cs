using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    private Controls _controls;
    private GameObject _currentCharacter;
    [SerializeField] private GameObject vampireCharacter;
    [SerializeField] private GameObject batCharacter;
    private PlayerController _vampireController;
    private BatController _batController;

    private void Awake()
    {
        _currentCharacter = vampireCharacter;
        _vampireController = vampireCharacter.GetComponent<PlayerController>();
        _batController = batCharacter.GetComponent<BatController>();
        _controls = new Controls();
        _controls.Game.SwitchCharacter.performed += OnSwitchCharacterPerformed;
    }

    private void OnEnable()
    {
        _controls.Game.Enable();
    }

    private void OnDisable()
    {
        _controls.Game.Disable();
    }

    private void OnSwitchCharacterPerformed(InputAction.CallbackContext context)
    {
        if (_currentCharacter == vampireCharacter)
        {
            _currentCharacter = batCharacter;
            _vampireController.controls.Player.Disable();
            _batController.controls.Bat.Enable();
        }
        else
        {
            _currentCharacter = vampireCharacter;
            _vampireController.controls.Player.Enable();
            _batController.controls.Bat.Disable();
        }
    }
}
