using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    [SerializeField] float bounceForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("BouncyTime");
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }
}
