using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    private Controls _controls;
    [HideInInspector] public GameObject currentCharacter;
    [HideInInspector] public GameObject vampireCharacter;
    [HideInInspector] public GameObject batCharacter;
    private PlayerController _vampireController;
    private BatController _batController;
    private MMF_Player _mmfPlayer;
    private Rigidbody2D _batRigidbody2D;
    private Rigidbody2D _vampireRigidbody2D;
    [SerializeField] private GameObject optionsCanvas;

    private void Awake()
    {
        vampireCharacter = FindObjectOfType<PlayerController>().gameObject;
        batCharacter = FindObjectOfType<BatController>().gameObject;
        currentCharacter = vampireCharacter;
        _vampireController = vampireCharacter.GetComponent<PlayerController>();
        _batController = batCharacter.GetComponent<BatController>();
        _vampireRigidbody2D = vampireCharacter.GetComponent<Rigidbody2D>();
        _batRigidbody2D = batCharacter.GetComponent<Rigidbody2D>();
        _controls = new Controls();
        _controls.Game.SwitchCharacter.performed += OnSwitchCharacterPerformed;
        _controls.Game.Options.performed += OnOptionsPerformed;
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

    private void OnOptionsPerformed(InputAction.CallbackContext context)
    {
        if (optionsCanvas != null)
            optionsCanvas.SetActive(!optionsCanvas.activeSelf);
    }

    private void OnSwitchCharacterPerformed(InputAction.CallbackContext context)
    {
        _mmfPlayer.PlayFeedbacks();
        LightController tempLightController = currentCharacter.GetComponent<LightController>();
        if (tempLightController.playerState == LightController.PlayerState.Interacting)
            tempLightController.lightSwitch.MechanismActivation(currentCharacter);
        
        if (currentCharacter == vampireCharacter)
        {
            currentCharacter = batCharacter;
            _vampireController.GetComponent<Animator>().SetBool("isActive", false);
            _vampireController.GetComponent<PlayerController>().shadowAnimator.SetBool("isActive", false);
            //CONTROLS
            _vampireController.controls.Player.Disable();
            _batController.controls.Bat.Enable();
            vampireCharacter.GetComponent<LightController>().controls.Disable();
            batCharacter.GetComponent<LightController>().controls.Enable();
            //END CONTROLS
            _batRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            _vampireRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            currentCharacter = vampireCharacter;
            _vampireController.GetComponent<Animator>().SetBool("isActive", true);
            _vampireController.GetComponent<PlayerController>().shadowAnimator.SetBool("isActive", true);
            //CONTROLS
            _vampireController.controls.Player.Enable();
            _batController.controls.Bat.Disable();
            vampireCharacter.GetComponent<LightController>().controls.Enable();
            batCharacter.GetComponent<LightController>().controls.Disable();
            //END CONTROLS
            _vampireRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            _batRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (currentCharacter.GetComponent<LightController>().playerState == LightController.PlayerState.Interacting)
            FindObjectOfType<CameraController>().ZoomOut(currentCharacter);
        else
            FindObjectOfType<CameraController>().ZoomIn(currentCharacter);
    }
}
