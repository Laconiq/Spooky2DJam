using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class LightSwitchManager : MonoBehaviour
{
    private Controls _controls;

    public bool _isInsideTrigger;
        
    [SerializeField] private Rigidbody2D _playerRigidbody;
    
    [SerializeField] private GameObject _text;

    [SerializeField] private GameObject _associatedLight;

    private void Awake()
    {
        _controls = new Controls();
        
        _controls.Player.Interaction.performed += MechanismActivation; 
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

    public void MechanismActivation(InputAction.CallbackContext context)
    {
        if (_isInsideTrigger)
        {
            _playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("Interaction is done");  
        }
    }
}

