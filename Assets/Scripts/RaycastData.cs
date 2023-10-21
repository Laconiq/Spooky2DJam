using UnityEngine;
public class RaycastData : MonoBehaviour
{
    private Vector3 initialChildRotation;
    private void Awake()
    {
        Transform child = transform.GetChild(0);
        initialChildRotation = child.localRotation.eulerAngles;
        Vector3 newRotation = new Vector3(0f, 0f, initialChildRotation.z);
        child.localRotation = Quaternion.Euler(newRotation);
    }
    public void ResetRotation()
    {
        Transform child = transform.GetChild(0);
        child.localRotation = Quaternion.Euler(initialChildRotation);
    }
}