using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCameraRotation : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cam;
    private CinemachineOrbitalTransposer transposer;
    [SerializeField] private PlayerInput player;

    private InputAction m_Rotate;

    private void OnValidate()
    {
        if (!cam) { cam = GetComponent<CinemachineVirtualCamera>(); }
        if (!player) { player = GetComponent<PlayerInput>(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        transposer = cam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Rotate = player.actions["rotate"];
        if (m_Rotate != null)
        {
            //Debug.Log(m_Rotate.ReadValue<float>());
            transposer.m_XAxis.m_InputAxisValue = m_Rotate.ReadValue<float>();
        }
    }
}
