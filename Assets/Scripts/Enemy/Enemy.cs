using System;
using System.Collections;
using UnityEngine;

public interface IEnemy
{
    public CharacterController Controller { get; }

    public Action OnDeath { get; set; }

    public float Speed { get ; set; }

    public void Move();
}


public class Enemy : MonoBehaviour, IEnemy
{

    [SerializeField] private CharacterController m_Controller;
    
    private GameObject m_Target;
    public CharacterController Controller => m_Controller;

    public Action OnDeath { get; set; }

    [Header("Movement Settings")]
    [SerializeField] private Animator m_Animator;
    [SerializeField, Range(0.1f, 10f)] private float m_MovementSpeed = 1f;
    private string m_AnimMoveSpeedParam = "MovementSpeed";

    public float Speed { get => m_MovementSpeed; set => m_MovementSpeed = value; }

    protected virtual void OnValidate()
    {
        if (!m_Controller) { m_Controller = GetComponent<CharacterController>(); }
    }


    protected virtual void Start()
    {
        // Inject this externally through EnemyManager
        m_Target = GameObject.FindGameObjectWithTag("Player");

        if (!m_Target)
        {
            Debug.LogWarning("Entity has no player to follow");
            Destroy(gameObject);
        }
    }

    /// <summary>
    ///     Uses the <see cref="CharacterController"/> to move the entity and respect collisions
    /// </summary>
    protected virtual void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        if (!m_Target || !m_Controller.enabled) { return; }

        transform.LookAt(m_Target.transform.position);

        Vector3 directionVector = m_MovementSpeed * Time.deltaTime * Vector3.Normalize(m_Target.transform.position - transform.position);
        m_Controller.Move(directionVector);

        // Animate movement
        m_Animator.SetFloat(m_AnimMoveSpeedParam, directionVector.magnitude);
    }

    public virtual void Hit(IProjectile projectile)
    {
        Die();
    }

    public virtual void Die()
    {
        m_Controller.enabled = false;
        StartCoroutine(DieCoroutine());
    }

    protected virtual IEnumerator DieCoroutine()
    {
        // Could play a death animation here
        //yield return new WaitForSeconds(1);

        yield return null;

        OnDeath?.Invoke();
    }
}
