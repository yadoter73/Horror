using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private EnemyAI _enemyAI;
    private Animator _animator;
    private Health _health;
    private void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
        _animator = GetComponent<Animator>();

        _enemyAI.OnAttack += AttackAnimation;
    }

    private void Update()
    {
        _animator.SetBool("IsRunning", _enemyAI.IsMoving);
    }

    private void AttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
}
