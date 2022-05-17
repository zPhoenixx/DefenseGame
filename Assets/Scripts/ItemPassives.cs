using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPassives : MonoBehaviour
{
    private int[] currentPassives = new int[6];
    void Start()
    {
        currentPassives[0] = 1;
        currentPassives[1] = 0;
        currentPassives[2] = 0;
        currentPassives[3] = 0;
        currentPassives[4] = 0;
        currentPassives[5] = 0;
    }

    // Update is called once per frame
    private void Empty()
    { 
    
    }
    private void Damage()
    {

    }
}
