using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineVirtualCameraBase))]
public class CameraInputActionMapping : MonoBehaviour
{

    [Header("Input Action References")]
    [SerializeField] private InputActionReference m_RotateAction;
    [SerializeField] private InputActionReference m_ZoomAction;

    [Header("Camera Height Boundaries")]
    public float HeightMax = 35; // unused
    public float HeightMin = 10;

    [Header("Camera Zoom Boundaries")]
    public float ZoomMax = 8;
    public float ZoomMin = 3;

    private CinemachineVirtualCamera cam;
    private CinemachineOrbitalTransposer transposer;

    private InputAction m_Rotate;
    private InputAction m_Zoom;

    private void OnValidate()
    {
        if (!cam) { cam = GetComponent<CinemachineVirtualCamera>(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        OnValidate();
        transposer = cam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        if (transposer == null )
        {
            Debug.LogWarning("No CinemachineOrbitalTransposer to handle rotations");
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_Rotate = m_RotateAction.action;
        m_Zoom = m_ZoomAction.action;

        DoRotate();

        DoZoom();
    }

    private void DoRotate()
    {
        if (m_Rotate == null) { return; }
        transposer.m_XAxis.m_InputAxisValue = m_Rotate.ReadValue<float>();
    }

    private void DoZoom()
    {
        if (m_Zoom == null) { return; }

        float zoomInput = m_Zoom.ReadValue<float>();
        if (zoomInput != 0f)
        {
            cam.m_Lens.OrthographicSize = Mathf.Clamp(
                cam.m_Lens.OrthographicSize - (zoomInput / 1000),
                ZoomMin,
                ZoomMax);
            // TODO update camera height based on zoom
        }

    }
}
