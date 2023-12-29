using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : ItemParent
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!PlayerStatManager.instance.isRunning) return;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            PlayerStatManager.instance.StartCoroutine(PlayerStatManager.instance.OnPlayerDie());
        }
    }
}
