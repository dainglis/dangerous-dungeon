using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class InputActionMapping : MonoBehaviour
{
    // Cache variables
    private Vector2 m_MoveCache;
    private Vector3 m_LocalForward;
    private Vector3 m_LocalLeft;

    private Vector3 m_Move;

    private Vector3 m_Target;

    private Plane m_TargetPlane;

    private float m_ProjectileElapsed;
    private bool m_MarkDirty = false;
    private bool m_CanFire = true;



    [SerializeField] private CharacterController m_Character;
    [SerializeField] private ProjectilePool m_ProjectilePool;

    //[SerializeField] private string ControlLayer = "Control";

    [Header("Movement Settings")]
    [SerializeField, Range(0.1f, 10f)] private float m_MovementSpeed = 1f;


    [Header("Projectile Settings")]
    [SerializeField] private bool m_AutoFire = false;
    [SerializeField, Range(0.1f, 10f)] private float m_ProjectileRate = 1f;

    [SerializeField, Range(0.1f, 10f)] private float m_ProjectileSpeed = 1f;

    private void OnValidate()
    {
        if (!m_Character) { GetComponent<CharacterController>(); }
    }

    private void Start()
    {
        // Define the raycast plane for "firing"
        m_TargetPlane = new(Vector3.up, m_Character.center);
    }


    void Update()
    {
        if (m_MarkDirty) { CalculateDirectionVectors(); }

        ApplyMovement();
        CheckProjectile();

        Debug.DrawRay(transform.position + m_Character.center, m_LocalForward, Color.red);
        Debug.DrawLine(gameObject.transform.position, m_Target, Color.blue);
    }



    public void CheckProjectile()
    {
        if (m_CanFire && (m_AutoFire || m_QueueProjectile))
        {
            LaunchProjectile();
            m_ProjectileElapsed = 0f;
            m_QueueProjectile = false;
            m_CanFire = false;
            return;
        }

        m_ProjectileElapsed += Time.deltaTime;

        if (m_ProjectileElapsed >= (1 / m_ProjectileRate))
        {
            m_CanFire = true;
        }
    }

    public void LaunchProjectile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (m_TargetPlane.Raycast(ray, out float distance))
        {
            m_Target = ray.GetPoint(distance);
        }

        m_ProjectilePool
            .Projectiles.Get()
            .SetTrajectory(transform.position + m_Character.center, m_Target, m_ProjectileSpeed)
            .Activate();
    }

    private void ApplyMovement()
    {
        m_Character.Move(m_MovementSpeed * Time.deltaTime * m_Move);
        if (m_Move != Vector3.zero) { gameObject.transform.forward = m_Move; }
    }

    /// <summary>
    ///     Calculates normalized 'Forward' and 'Left' vectors based on <see cref="Camera.main"/>
    /// </summary>
    private void CalculateDirectionVectors()
    {
        // Calculate forward
        m_LocalForward = gameObject.transform.position - Camera.main.transform.position;
        m_LocalForward = Vector3.Normalize(new Vector3(m_LocalForward.x, 0, m_LocalForward.z));
        m_LocalLeft = Vector3.Cross(m_LocalForward, Vector3.up).normalized;

        // m_MoveCache.y is Z-forward, m_MoveCache.x is right
        m_Move = (m_LocalForward * m_MoveCache.y) + (m_LocalLeft * -m_MoveCache.x);
    }

    #region Input System Callbacks

    bool m_QueueProjectile = false;

    public void OnFire()
    {
        m_AutoFire = !m_AutoFire;
    }

    /// <summary>
    ///     Toggles auto-fire
    /// </summary>
    public void OnToggleFire()
    {
        m_AutoFire = !m_AutoFire;
        m_ProjectileElapsed = 0f;
    }

    public void OnRotate(InputValue value)
    {
        float activeRotation = value.Get<float>();

        Debug.Log($"Rotate: {activeRotation}");

        m_MarkDirty = activeRotation != 0f;
    }

    /// <summary>
    ///     Handle movement input
    /// </summary>
    /// <param name="value"></param>
    public void OnMove(InputValue value)
    {
        // Get input from InputSystem
        m_MoveCache = value.Get<Vector2>();

        CalculateDirectionVectors();
    }

    #endregion
}
