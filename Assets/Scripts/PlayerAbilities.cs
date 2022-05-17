using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private int[] currentAbilities = new int[6];

    public GameObject shadowBall;

    private PlayerMotor motor;
    private PlayerStats stats;
    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        motor = GetComponent<PlayerMotor>();
    }
    void Start()
    {
        currentAbilities[0] = 2;
        currentAbilities[1] = 0;
        currentAbilities[2] = 0;
        currentAbilities[3] = 0;
        currentAbilities[4] = 1;
        currentAbilities[5] = 0;
    }

    void Update()
    {
        
    }
    public void UseAbility(int abilityID)
    {
        switch (currentAbilities[abilityID])
        {
            case 0:
                Empty();
                break;
            case 1:
                DashAbility();
                break;
            case 2:
                ShadowBall();
                break;
        }
    }
    private void DashAbility()
    {
        float maxDashDistance = 5f;
        if (stats.UseManaForAbility(5))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(transform.position, hit.point) > maxDashDistance)
                {
                    Vector3 dir = (transform.position - hit.point).normalized;
                    motor.SetLocation(transform.position - (dir * maxDashDistance), 0, 4, false);
                }
                else
                {
                    motor.SetLocation(hit.point, 0, 4, false);
                }
            }
        }
    }
    private void ShadowBall()
    {
        if (stats.UseManaForAbility(20))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 dir = (transform.position - hit.point).normalized;
                GameObject ball = Instantiate(shadowBall, transform.position - (dir * 2), Quaternion.identity);
                motor.FacePosition(transform.position - dir);
                ball.GetComponent<ShadowBall>().SetTargetPos(hit.point);
            }
        }
    }
    private void Empty()
    { 
    
    }
}
