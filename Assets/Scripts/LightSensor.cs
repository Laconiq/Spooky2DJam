using UnityEngine;
public class LightSensor : MonoBehaviour
{
    [SerializeField] private GameObject objectToEnable;
    private BoxCollider2D _boxCollider2D;
    private int _activeLightCount;
    private bool _isSensorEnabled;
    private void Awake()
    {
        _boxCollider2D = objectToEnable.GetComponent<BoxCollider2D>();
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
            Debug.Log("Light Sensor Activé");
            _boxCollider2D.enabled = false;
            // À rajouter plus tard, l'animation qui active le mécanisme
        }
        else
        {
            Debug.Log("Light Sensor Désactivé");
            _boxCollider2D.enabled = true;
            // À rajouter plus tard, l'animation qui désactive le mécanisme
        }
    }
}