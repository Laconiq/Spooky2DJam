using Cinemachine;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    private Controls _controls;
    private GameObject _currentCharacter;
    private GameObject _vampireCharacter;
    private GameObject _batCharacter;
    private PlayerController _vampireController;
    private BatController _batController;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private MMF_Player _mmfPlayer;

    private void Awake()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _vampireCharacter = FindObjectOfType<PlayerController>().gameObject;
        _batCharacter = FindObjectOfType<BatController>().gameObject;
        _currentCharacter = _vampireCharacter;
        _vampireController = _vampireCharacter.GetComponent<PlayerController>();
        _batController = _batCharacter.GetComponent<BatController>();
        _controls = new Controls();
        _controls.Game.SwitchCharacter.performed += OnSwitchCharacterPerformed;
        _mmfPlayer = GetComponent<MMF_Player>();

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
        _mmfPlayer.PlayFeedbacks();
        if (_currentCharacter == _vampireCharacter)
        {
            _currentCharacter = _batCharacter;
            _vampireController.controls.Player.Disable();
            _batController.controls.Bat.Enable();
            _cinemachineVirtualCamera.Follow = _batCharacter.transform;
        }
        else
        {
            _currentCharacter = _vampireCharacter;
            _vampireController.controls.Player.Enable();
            _batController.controls.Bat.Disable();
            _cinemachineVirtualCamera.Follow = _vampireCharacter.transform;
        }
    }
}
