using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissle : MonoBehaviour
{
    public Transform target;

    public float speed = 10f;
    public float rotateSpeed = 200f;
    public float explosionRadius = 2f;
    public LayerMask playerLayer;

    public GameObject explosionParticles;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            Vector2 dir = (Vector2)target.position - rb.position;
            dir.Normalize();

            float rotateAmt = Vector3.Cross(dir, transform.up).z;

            rb.angularVelocity = -rotateAmt * rotateSpeed;

            rb.velocity = transform.up * speed;
        }
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
