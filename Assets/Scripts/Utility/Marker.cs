using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    private float rSpeed = 1f;
    void Update()
    {
        transform.Rotate(Vector3.up * rSpeed);
    }
}
