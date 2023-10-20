using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class LightSwitchManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRigidbody;
    
    [SerializeField] private GameObject _text;

    [SerializeField] private GameObject _associatedLight;
    
    // Start is called before the first frame update
    void Start()
    {
        _text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _text.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _text.SetActive(false);
    }

    private void MechanismActivation()
    {
        _playerRigidbody.velocity = Vector2.zero;
    }
}

