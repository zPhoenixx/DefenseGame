using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMotor : MonoBehaviour
{
    private float moveSpeed;
    public Vector3[] locations;
    public Vector3 nextLocation;
    public Vector3 finalLocation;
    public int locationCounter = 0;

    public bool hasPath = false;
    public bool inRadius = false;
    public float followRadius = 0f;
    void Update()
    {
        if (hasPath)
        {
            DistanceCheck();
            if (!inRadius)
            {
                FacePosition(nextLocation);
                transform.position = Vector3.MoveTowards(transform.position, nextLocation, Time.deltaTime * moveSpeed);
                if (DestinationReached())
                {
                    NextDestination();
                }
            }
            else
            {
                FacePosition(finalLocation);
                hasPath = false;
            }
        }
    }
    public void SetLocation(Vector3 _pos, float _radius)
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, _pos, NavMesh.AllAreas, path))
        {
            finalLocation = _pos;
            followRadius = _radius;
            inRadius = false;
            locationCounter = 0;
            locations = path.corners;
            nextLocation = locations[0];
            hasPath = true;
        }
    }
    private void DistanceCheck()
    {
        if (Vector3.Distance(transform.position, finalLocation) <= followRadius)
        {
            inRadius = true;
        }
        else
        {
            inRadius = false;
        }
    }
    private void NextDestination()
    {
        if (locations.Length > locationCounter + 1)
        {
            nextLocation = locations[locationCounter + 1];
            locationCounter++;
        }
        else
        {
            hasPath = false;
        }
    }
    private bool DestinationReached()
    {
        if (Vector3.Distance(transform.position, nextLocation) < 0.1f)
            return true;
        else
            return false;
    }
    private void FacePosition(Vector3 pos)
    {
        transform.LookAt(new Vector3(pos.x, transform.position.y, pos.z));
    }
    public void SetMoveSpeed(float _moveSpeed)
    {
        moveSpeed = _moveSpeed;
    }
}
