using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class LightController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Interacting
    }
    public PlayerState playerState = PlayerState.Idle;
    
    [HideInInspector] public Controls controls;
    [SerializeField] private bool _isInsideTrigger;
    [SerializeField] private LightSwitch lightSwitch;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.Interaction.performed += MechanismActivation;
        controls.Player.RotateLight.performed += RotateLight;
        controls.Player.RotateLight.canceled += CancelLightRotation;
    }
    
    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }
    
    private void OnDestroy()
    {
        controls.Player.Interaction.performed -= MechanismActivation;
        controls.Player.RotateLight.performed -= RotateLight;
        controls.Player.RotateLight.canceled -= CancelLightRotation;
    }

    private void MechanismActivation(InputAction.CallbackContext context)
    {
        if (lightSwitch == null) return;
        lightSwitch.MechanismActivation(gameObject);
    }
    
    private void RotateLight(InputAction.CallbackContext context)
    {
        if (playerState is PlayerState.Interacting)
        {
            lightSwitch.objectIsRotating = true;
            lightSwitch.lightRotation = context.ReadValue<Vector3>().z;
        }
    }
    
    private void CancelLightRotation(InputAction.CallbackContext context)
    {
        if (lightSwitch == null) return;
        lightSwitch.objectIsRotating = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        LightSwitch lightSwitch = other.GetComponent<LightSwitch>();
        if (lightSwitch == null) return;
        _isInsideTrigger = true;
        this.lightSwitch = lightSwitch;
        this.lightSwitch.DisplayText(1);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        LightSwitch lightSwitch = other.GetComponent<LightSwitch>();
        if (lightSwitch == null) return;
        this.lightSwitch.DisplayText(0);
        _isInsideTrigger = false;
        this.lightSwitch = null;
    }
}