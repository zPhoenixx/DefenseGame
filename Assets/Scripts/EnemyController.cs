using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float attackRange;
    private float attackSpeed;
    private float lastAttack = 0f;
    private bool canAttack = true;

    private float aggroRange = 10f;
    private float walkRadius = 10f;

    public GameObject target;
    public GameObject player;

    private EnemyMotor motor;
    private EnemyStats stats;
    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        motor = GetComponent<EnemyMotor>();
    }
    private void Update()
    {
        if (!canAttack)
        {
            if (Time.time - lastAttack >= attackSpeed)
            {
                canAttack = true;
            }
        }
        if (target == player || target == null)
            CheckDistanceFromPlayer();
        if (target != null)
        {
            if (TargetInRange())
                AttackTarget();
            else if (!motor.hasPath)
                FollowTarget();
        }
        if (target == null)
        {
            if (!motor.hasPath)
            {
                Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
                randomDirection += transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
                Vector3 finalPosition = hit.position;
                motor.SetLocation(finalPosition, 0);
            }
        }
    }
    private void AttackTarget()
    {
        if (canAttack)
        {
            stats.AttackTarget(target);
            lastAttack = Time.time;
            canAttack = false;
        }
    }
    private void FollowTarget()
    {
        motor.SetLocation(target.transform.position, attackRange);
    }
    private bool TargetInRange()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
            return true;
        else
            return false;
    }
    private void CheckDistanceFromPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= aggroRange)
            target = player;
        else
            target = null;
    }
    public void SetStats(float _attackSpeed, float _attackRange)
    {
        attackSpeed = _attackSpeed;
        attackRange = _attackRange;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
