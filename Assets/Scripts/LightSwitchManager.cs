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

    public PlayerController _playerController;

    private Vector3 _lightRotation;
    private float _lightRotationSpeed = 5f;

        
    [SerializeField] private Rigidbody2D _playerRigidbody;
    
    [SerializeField] private GameObject _text;

    [SerializeField] private GameObject _associatedLight;

    private void Awake()
    {
        _controls = new Controls();
        
        _controls.Player.Interaction.performed += MechanismActivation;
        _controls.Player.RotateLightUp.performed += context => { _lightRotation = context.ReadValue<Vector3>(); };
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
        _controls.Player.RotateLightUp.performed -= RotateLightUp;
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Body enter");
        
        _text.SetActive(true);

        _isInsideTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Body exit");
        
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

    public void RotateLightUp(InputAction.CallbackContext context)
    {
        if (_playerController.playerState is PlayerController.PlayerState.Interacting)
        {
            _lightRotation = context.ReadValue<Vector3>();
            
            Debug.Log("Rotate");
           
            _associatedLight.transform.Rotate(Vector3.forward, _lightRotationSpeed);
        }
    }
}

