using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.tvOS;


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



    [SerializeField] private CharacterController character;

    //[SerializeField] private string ControlLayer = "Control";

    [Header("Movement Settings")]
    [SerializeField, Range(0.1f, 10f)] private float m_MovementSpeed = 1f;


    [Header("Projectile Settings")]
    [SerializeField] private bool m_AutoFire = false;
    [SerializeField, Range(0.1f, 10f)] private float m_ProjectileRate = 1f;

    [SerializeField, Range(0.1f, 10f)] private float m_ProjectileSpeed = 1f;

    private void OnValidate()
    {
        if (!character) { GetComponent<CharacterController>(); }
    }

    private void Start()
    {
        // Define the raycast plane for "firing"
        m_TargetPlane = new(Vector3.up, character.center);
    }


    void Update()
    {
        if (m_MarkDirty) { CalculateDirectionVectors(); }

        ApplyMovement();
        CheckProjectile();

        Debug.DrawRay(transform.position + character.center, m_LocalForward, Color.red);
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
            Debug.Log("Hit");
            m_Target = ray.GetPoint(distance);
        }

        _ = Projectile.CreateProjectile(transform.position + character.center, m_Target, m_ProjectileSpeed);
    }

    private void ApplyMovement()
    {
        character.Move(m_MovementSpeed * Time.deltaTime * m_Move);
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

        // TODO figure out why x and z are swapped, and why x is negative
        m_Move = (m_LocalForward * m_MoveCache.y) + (m_LocalLeft * -m_MoveCache.x);
    }

    #region Input System Callbacks

    bool m_QueueProjectile = false;

    public void OnFire()
    {
        m_AutoFire = !m_AutoFire;
        //m_QueueProjectile = true;
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
        float item = value.Get<float>();

        Debug.Log($"Rotate: {item}");

        m_MarkDirty = item != 0f;
        //CalculateDirectionVectors();
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
