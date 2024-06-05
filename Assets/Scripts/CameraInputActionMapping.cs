using Cinemachine;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;


/// <summary>
///     Maps Input Actions to Orbital Transposer camera movements
/// </summary>
/// <remarks>
///     This will need to be significantly modified if upgrading to Cinemachine 3.x
/// </remarks>
[RequireComponent(typeof(CinemachineVirtualCameraBase))]
public class CameraInputActionMapping : MonoBehaviour
{
    [Header("Camera Height Boundaries")]
    public float HeightMax = 35; // unused
    public float HeightMin = 10;

    [Header("Camera Zoom Boundaries")]
    public float ZoomMax = 8;
    public float ZoomMin = 3;
    public float ZoomSensitivity = 3;

    private CinemachineVirtualCamera cam;
    private CinemachineOrbitalTransposer transposer;

    private void OnValidate()
    {
        if (!cam) { cam = GetComponent<CinemachineVirtualCamera>(); }
    }

    private void Start()
    {
        OnValidate();
        transposer = cam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        if (transposer == null)
        {
            Debug.LogWarning("No CinemachineOrbitalTransposer to handle rotations");
        }
    }

    public void OnRotate(CallbackContext context)
    {
        transposer.m_XAxis.m_InputAxisValue = context.ReadValue<float>();
    }

    public void OnZoom(CallbackContext context)
    {
        float zoomInput = context.ReadValue<float>();

        if (zoomInput != 0f)
        {
            cam.m_Lens.OrthographicSize = Mathf.Clamp(
                cam.m_Lens.OrthographicSize - (zoomInput / ZoomSensitivity),
                ZoomMin,
                ZoomMax);
            // TODO update camera height based on zoom
        }
    }
}
