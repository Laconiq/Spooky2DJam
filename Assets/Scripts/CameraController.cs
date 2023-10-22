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

    public void ZoomIn()
    {
        _cinemachineVirtualCamera.m_Lens.OrthographicSize = zoomIn;
    }

    public void ZoomOut()
    {
        _cinemachineVirtualCamera.m_Lens.OrthographicSize = zoomOut;
    }
}