using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform Player;
    private NavMeshAgent _agent;

    [Header("Patrol Settings")]
    public Transform PatrolCenter;
    public float PatrolRadius = 20f;
    public float PatrolWaitTime = 2f;

    [Header("Detection Settings")]
    [SerializeField] private float _detectionRange = 20f;
    [SerializeField] private float _attackRange = 10f;
    [SerializeField] private float _viewAngle = 90f;
    [SerializeField] private LayerMask _wallsAndPlayerLayer;
    [SerializeField] private float _searchDuration;

    [SerializeField] private Transform _head;

    [Header("Fight Settings")]
    [SerializeField] private float _attackRate = 1f;
    [SerializeField] private float _attackDamage = 20f;
    private bool _isAttacking = false;

    private EnemyState _currentState;
    private float _distanceToPlayer;
    private Vector3 _lastKnownPosition;
    private bool _isMoving;

    public bool IsMoving => _isMoving;
    public float SearchDuration => _searchDuration;
    public float ViewAngle => _viewAngle;

    public event Action OnAttack;

    private float _agentSpeed;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agentSpeed = _agent.speed;
        IsPlayerInFieldOfView();
        _currentState = new PatrolState(this);

    }

    private void Update()
    {
        _distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        _isMoving = _agent.velocity.magnitude > _agent.speed / 4;
        _currentState?.UpdateState();

        _agent.speed = _isAttacking ? 0 : _agentSpeed;
       
    }

    public void SwitchState(EnemyState newState)
    {
        _currentState = newState;
    }

    public void MoveTo(Vector3 destination)
    {
        if (_agent != null && _agent.isActiveAndEnabled)
        {
            _agent.SetDestination(destination);
        }
    }

    public bool IsPlayerInAttackRange() => _distanceToPlayer <= _attackRange;

    public bool IsPlayerInFieldOfView(float FOV = -1)
    {
        if (FOV < 0)
        {
            FOV = _viewAngle;
        }


        Vector3 directionToPlayer = (Player.position - _head.position).normalized;
        float angle = Vector3.Angle(_head.forward, directionToPlayer);

        if (angle > FOV / 2 || !(_distanceToPlayer <= _detectionRange))
        {
            return false;
        }

        if (Physics.Raycast(_head.position, directionToPlayer, out RaycastHit hit, _detectionRange, _wallsAndPlayerLayer))
        {
            if (hit.transform != Player)
            {
                return false;
            }
        }

        return true;
    }

    public void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = (Player.position - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        targetRotation.x = transform.rotation.x;
        targetRotation.z = transform.rotation.z;

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * _agent.angularSpeed / 100
        );
    }


    public void AttackPlayer()
    {
        if (!_isAttacking)
        {

            OnAttack?.Invoke();
            StartCoroutine(AttackRoutine());
        }

    }


    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        Player.GetComponent<Health>().TakeDamage(_attackDamage);
        yield return new WaitForSeconds(_attackRate);
        _isAttacking = false;

    }

    public void UpdateLastKnownPosition(Vector3 position)
    {
        _lastKnownPosition = position;
    }

    public void Die()
    {
        _agent.enabled = false;
        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Color color = Color.red;
        color.a = 0.3f;
        Gizmos.color = color;

        Gizmos.DrawSphere(PatrolCenter.position, PatrolRadius);

        color = Color.green;
        color.a = 0.3f;
        Gizmos.color = color;

        Gizmos.DrawSphere(transform.position, _attackRange);
    }
}
