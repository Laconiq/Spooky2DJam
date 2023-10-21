using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = System.Numerics.Vector2;


public class LightSwitchManager : MonoBehaviour
{
    private Controls _controls;

    private bool _isInsideTrigger;
    private bool _isPlayerFrozen;
    private bool _objectIsRotating;

    private PlayerController _playerController;

    private float _lightRotation;
    private readonly float _lightRotationSpeed = 10f;
    private float _maxUpRotation = 90f;
    private float _maxDownRotation = 0f;
    
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _associatedLight;


    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _controls = new Controls();
        _controls.Player.Interaction.performed += MechanismActivation;
        _controls.Player.RotateLightUp.performed += RotateLight;
        _controls.Player.RotateLightUp.canceled += CancelLightRotation;
    }
    
    private void OnEnable()
    {
        _controls.Player.Enable();
    }
    private void OnDisable()
    {
        _controls.Player.Disable();
    }
    
    private void OnDestroy()
    {
        _controls.Player.Interaction.performed -= MechanismActivation;
        _controls.Player.RotateLightUp.performed -= RotateLight;
        _controls.Player.RotateLightUp.canceled -= CancelLightRotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        _text.SetActive(false);
        _isInsideTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputToDisable = _controls.Player.RotateLightUp.ReadValue<Vector3>();
        
        if (_objectIsRotating)
        {
            Vector3 lightRotationAngle = _associatedLight.transform.localEulerAngles;

            float clampedZRotation = Mathf.Clamp(lightRotationAngle.z + _lightRotation * _lightRotationSpeed * Time.deltaTime, _maxDownRotation, _maxUpRotation);
            
            _associatedLight.transform.localEulerAngles = new Vector3(lightRotationAngle.x, lightRotationAngle.y, clampedZRotation);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _text.SetActive(true);

        _isInsideTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _text.SetActive(false);
        
        _isInsideTrigger = false;
    }

    private void MechanismActivation(InputAction.CallbackContext context)
    {
        if (_isInsideTrigger && _playerController.playerState is PlayerController.PlayerState.Idle)
        {
            _playerController.speed = 0;
 
            Debug.Log("Interaction is done");

            _playerController.playerState = PlayerController.PlayerState.Interacting;
        }
        else if(_playerController.playerState is PlayerController.PlayerState.Interacting)
        {
            _playerController.speed = 10;

            _playerController.playerState = PlayerController.PlayerState.Idle;
        }
    }

    private void RotateLight(InputAction.CallbackContext context)
    {
        if (_playerController.playerState is PlayerController.PlayerState.Interacting)
        {
            _objectIsRotating = true;
            _lightRotation = context.ReadValue<Vector3>().z;
        }
    }

    private void CancelLightRotation(InputAction.CallbackContext context)
    {
        _objectIsRotating = false;
    }
}

