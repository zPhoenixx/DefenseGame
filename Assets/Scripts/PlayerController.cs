using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float interactRange = 2f;
    private float attackRange;
    private float attackSpeed;
    private float lastAttack = 0f;
    private bool canAttack = true;
    private GameObject targetEnemy = null;
    private GameObject targetItem = null;

    public LayerMask clickMask;
    private Camera cam;
    private PlayerMotor motor;
    private PlayerStats stats;
    private PlayerInventory inventory;
    private PlayerAbilities abilityManager;
    public TargetUIManager targetUI;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        motor = GetComponent<PlayerMotor>();
        stats = GetComponent<PlayerStats>();
        abilityManager = GetComponent<PlayerAbilities>();
        cam = Camera.main;
    }
    void Update()
    {
        if (!canAttack)
        {
            if (Time.time - lastAttack >= 1 / attackSpeed)
            {
                canAttack = true;
            }
        }
        if (targetItem != null)
        {
            if (TargetInRange(targetItem, interactRange))
            {
                PickUpItem();
            }
            else if (!motor.hasPath)
            {
                FollowTarget(targetItem, interactRange);
            }
        }
        if (targetEnemy != null)
        {
            if (TargetInRange(targetEnemy, attackRange))
                AttackTarget();
            else if (!motor.hasPath)
                FollowTarget(targetEnemy, attackRange);
        }
        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();
        }
        HandleAbilities();
    }
    private void FollowTarget(GameObject _target, float _radius)
    {
        motor.SetLocation(_target.transform.position, _radius, 1, false);
    }
    private bool TargetInRange(GameObject _target,float _radius)
    {
        if (Vector3.Distance(transform.position, _target.transform.position) <= _radius)
            return true;
        else
            return false;
    }
    private void AttackTarget()
    {
        if (canAttack)
        {
            stats.AttackTarget(targetEnemy);
            lastAttack = Time.time;
            canAttack = false;
        }
    }
    private void PickUpItem()
    {
        if (inventory.AddItem(targetItem.GetComponent<GroundItem>().item))
            Destroy(targetItem.gameObject);
        targetItem = null;
    }
    public void SetStats(float _attackSpeed, float _attackRange)
    {
        attackSpeed = _attackSpeed;
        attackRange = _attackRange;
    }
    private void HandleRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, clickMask))
            {
                switch (hit.collider.tag)
                {
                    case "Enemy":
                        {
                            Debug.Log("Enemy");
                            targetEnemy = hit.collider.gameObject;
                            targetUI.SetTarget(targetEnemy.GetComponent<EnemyStats>());
                            motor.SetLocation(targetEnemy.transform.position, attackRange, 1, false);
                            break;
                        }
                    case "Ground":
                        {
                            Debug.Log("hit " + hit.collider.name + " at " + hit.point);
                            targetEnemy = null;
                            targetItem = null;
                            targetUI.RemoveTarget();
                            motor.SetLocation(hit.point, 0, 1, true);
                            break;
                        }
                    case "GroundItem":
                        {
                            Debug.Log("GroundItem");
                            targetEnemy = null;
                            targetItem = hit.collider.gameObject;
                            targetUI.RemoveTarget();
                            motor.SetLocation(hit.transform.position, interactRange, 1, false);
                            break;
                        }
                }
            }
        }
    }
    private void HandleAbilities()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            abilityManager.UseAbility(0);
        if (Input.GetKeyDown(KeyCode.W))
            abilityManager.UseAbility(1);
        if (Input.GetKeyDown(KeyCode.E))
            abilityManager.UseAbility(2);
        if (Input.GetKeyDown(KeyCode.R))
            abilityManager.UseAbility(3);
        if (Input.GetKeyDown(KeyCode.D))
            abilityManager.UseAbility(4);
        if (Input.GetKeyDown(KeyCode.F))
            abilityManager.UseAbility(5);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
