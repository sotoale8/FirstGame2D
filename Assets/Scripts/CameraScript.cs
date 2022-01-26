using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject RB;
    
    void Update()
    {
        Vector3 position = transform.position;
        position.x = RB.transform.position.x;
        position.y = RB.transform.position.y+3.0f;


        transform.position= position;
    }
}
