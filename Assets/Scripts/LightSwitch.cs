using System;
using UnityEngine;
using MoreMountains.Feedbacks;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _associatedLight;
    [SerializeField] private GameObject _text;
    [HideInInspector] public float lightRotation;
    private readonly float _lightRotationSpeed = 20f;
    private float _maxUpRotation = 90f;
    private float _maxDownRotation = 0f;
    
    [SerializeField] private MMF_Player onMmfPlayer;
    [SerializeField] private MMF_Player offMmfPlayer;
    private bool _isUsed;
    private GameObject _gameObjectUsing;
    [HideInInspector] public bool objectIsRotating;
    private Animator _wheelAnimator;

    private void Awake()
    {
        _text.SetActive(false);
        _wheelAnimator = gameObject.GetComponent<Animator>();
    }

    public void MechanismActivation(GameObject go)
    {
        if (!_isUsed)
        {
            onMmfPlayer.PlayFeedbacks();
            _isUsed = true;
            _gameObjectUsing = go;
            go.GetComponent<LightController>().playerState = LightController.PlayerState.Interacting;
            if (go.GetComponent<PlayerController>()!=null)
                go.GetComponent<PlayerController>().currentSpeed = 0;
            else if (go.GetComponent<BatController>() != null)
            {
                go.GetComponent<BatController>().currentSpeed = 0;
                FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        else if (_isUsed && _gameObjectUsing == go)
        {
            offMmfPlayer.PlayFeedbacks();
            _isUsed = false;
            lightRotation = 0;
            go.GetComponent<LightController>().playerState = LightController.PlayerState.Idle;
            if (go.GetComponent<PlayerController>()!=null)
                go.GetComponent<PlayerController>().currentSpeed = go.GetComponent<PlayerController>().speed;
            else if (go.GetComponent<BatController>() != null)
            {
                go.GetComponent<BatController>().currentSpeed = go.GetComponent<BatController>().speed;
                FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
    
    public void DisplayText(int i)
    {
        if (i == 1)
            _text.SetActive(true);
        else if (i == 0)
            _text.SetActive(false);
    }

    private void Update()
    {
        WheelAnimation();
        if (!objectIsRotating) return;
        Transform currentLight = _associatedLight.transform.GetChild(0);
        Vector3 lightRotationAngle = currentLight.localEulerAngles;
        float clampedZRotation = Mathf.Clamp(lightRotationAngle.z + lightRotation * _lightRotationSpeed * Time.deltaTime, _maxDownRotation, _maxUpRotation);
        currentLight.localEulerAngles = new Vector3(lightRotationAngle.x, lightRotationAngle.y, clampedZRotation);
    }

    private void WheelAnimation()
    {
        if (lightRotation == -1)
            _wheelAnimator.SetInteger("State", 1);
        else if (lightRotation == 1)
            _wheelAnimator.SetInteger("State", 2);
        else if (lightRotation == 0 || _isUsed == true) 
            _wheelAnimator.SetInteger("State", 0);
    }
}
