using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WinFlag : MonoBehaviour
{
    public string targetScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && PlayerStatManager.instance.isRunning)
        {
            Debug.Log("Level WIN!");
            PlayerStatManager.instance.StartCoroutine(PlayerStatManager.instance.OnWin(targetScene));
        }
    }
}
