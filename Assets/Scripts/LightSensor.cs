using UnityEngine;
public class LightSensor : MonoBehaviour
{
    [SerializeField] private GameObject objectToEnable;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    private int _activeLightCount;
    private bool _isSensorEnabled;
    private void Awake()
    {
        _boxCollider2D = objectToEnable.GetComponent<BoxCollider2D>();
        _animator = objectToEnable.GetComponent<Animator>();
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Raycast"))
        {
            _activeLightCount--;
            if (_activeLightCount <= 0 && _isSensorEnabled)
            {
                LightSensorSwitch(false);
                _isSensorEnabled = false;
            }
        }
    }
    private void LightSensorSwitch(bool isEnabled)
    {
        if (isEnabled)
        {
            _boxCollider2D.enabled = false;
            if (_animator == null) return;
            _animator.SetBool("isEnable", true);
        }
        else
        {
            _boxCollider2D.enabled = true;
            if (_animator == null) return;
            _animator.SetBool("isEnable", false);
        }
    }
}