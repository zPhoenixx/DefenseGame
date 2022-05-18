using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBall : MonoBehaviour
{
    private Vector3 targetPos;
    private float lifespan = 5f;
    private float spawntime;
    private float moveSpeed = 2f;
    private void Start()
    {
        spawntime = Time.time;
    }
    void Update()
    {
        if ((Time.time - spawntime >= lifespan) || Vector3.Distance(transform.position, targetPos) <= 0.1f)
            Detonate();
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
        moveSpeed *= 1.1f;
    }
    private void Detonate()
    {
        Destroy(gameObject);
    }
    public void SetTargetPos(Vector3 pos)
    {
        targetPos = pos;
    }
}
