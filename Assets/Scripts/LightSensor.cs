using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;

public class LightSensor : MonoBehaviour
{
    [SerializeField] private GameObject objectToEnable;
    [SerializeField] private Light2D _light2D;
    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    private MMF_Player _mmfPlayer;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    private int _activeLightCount;
    private bool _isSensorEnabled;
    private void Awake()
    {
        _boxCollider2D = objectToEnable.GetComponent<BoxCollider2D>();
        _animator = objectToEnable.GetComponent<Animator>();
        _mmfPlayer = GetComponent<MMF_Player>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Raycast"))
        {
            _activeLightCount++;
            if (_activeLightCount > 0 && !_isSensorEnabled)
            {
                LightSensorSwitch(true);
                _isSensorEnabled = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Raycast")) return;
        _activeLightCount--;
        if (_activeLightCount > 0 || !_isSensorEnabled) return;
        LightSensorSwitch(false);
        _isSensorEnabled = false;
    }
    private void LightSensorSwitch(bool isEnabled)
    {
        if (isEnabled)
        {
            _boxCollider2D.enabled = false;
            if (_animator == null) return;
            _animator.SetBool("isEnable", true);
            _light2D.intensity = maxIntensity;
            _mmfPlayer.PlayFeedbacks();
        }
        else
        {
            _boxCollider2D.enabled = true;
            if (_animator == null) return;
            _animator.SetBool("isEnable", false);
            _light2D.intensity = minIntensity;
        }
    }
}