using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawHandle : MonoBehaviour
{
    public float rotateSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), rotateSpeed);    
    }
}
