using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private int zoomIn;
    [SerializeField] private int zoomOut;

    private void Awake()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ZoomIn(GameObject go)
    {
        _cinemachineVirtualCamera.m_Lens.OrthographicSize = zoomIn;
        Follow(go);
    }

    public void ZoomOut(GameObject go)
    {
        _cinemachineVirtualCamera.m_Lens.OrthographicSize = zoomOut;
        Follow(go);
    }

    public void Follow(GameObject go)
    {
        if (go.GetComponent<LightController>().playerState == LightController.PlayerState.Interacting)
        {
            _cinemachineVirtualCamera.Follow = go.GetComponent<LightController>().lightSwitch._associatedLight.transform;
        }
        else
        {
            _cinemachineVirtualCamera.Follow = go.transform;
        }
    }
}