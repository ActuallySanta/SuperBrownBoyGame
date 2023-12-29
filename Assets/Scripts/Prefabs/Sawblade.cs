using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblade : DamagingObject
{
    public float rotateSpeed = 4f;
    float rotateMult;
    public override void Start()
    {
        rotateMult = Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    public override void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), rotateSpeed * rotateMult);
    }


}
