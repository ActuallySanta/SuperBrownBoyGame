using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StandardMissle : MonoBehaviour
{
    public float speed = 10f;
    public float explosionRadius = 2f;
    public LayerMask playerLayer;

    public GameObject explosionParticles;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        rb.velocity = transform.up * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Physics2D.OverlapCircle(transform.position, explosionRadius, playerLayer))
        {
            StartCoroutine(PlayerStatManager.instance.OnPlayerDie());
        }
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
