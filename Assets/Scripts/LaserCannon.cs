using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannon : ItemParent
{
    public float fireTimer, rayDistance, lookLerpSpeed, resetTimer,dodgeTimer;
    public LayerMask playerLayer, noSeeThroughLayer,groundLayer;

    public Transform laserCannon;
    public Transform firePoint;

    GameObject player;
    [SerializeField] GameObject laserParticle;
    [SerializeField] GameObject explosionParticle;

    [SerializeField] LineRenderer lineRenderer;

    bool canSeePlayer;
    bool canFire;

    bool isCharging;

    Vector2 directionToTarget;

    enum FireMode
    {
        Tracking,
        StraightAhead,
    }

    [SerializeField] FireMode currFireMode;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        isCharging = false;
        canFire = true;
        firePoint.parent = null;
    }

    // Update is called once per frame
    public override void  Update()
    {
        base.Update();

        Debug.DrawRay(transform.position, directionToTarget * 100f, Color.green);
        bool canSeePlayer = CheckForPlayer();
        if (player != null && canSeePlayer)
        {
            if (canFire)
            {
                isCharging = true;
                canFire = false;
                StartCoroutine(FireLaser());
            }
            // Get the direction to the target object
            if (currFireMode == FireMode.Tracking)
            {
                if (isCharging)
                {
                    directionToTarget = (player.transform.position - transform.position);
                }
                laserCannon.transform.up = Vector2.Lerp(laserCannon.transform.up, directionToTarget, lookLerpSpeed);
            }
            else
            {
                directionToTarget = transform.up;
            }

            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, directionToTarget, 1000f, groundLayer);
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, _hit.point);
        }
        
    }

    bool CheckForPlayer()
    {
        Collider2D[] checkedObjects = Physics2D.OverlapCircleAll(transform.position, 100f, playerLayer);

        if (checkedObjects.Length > 0)
        {
            player = checkedObjects[0].gameObject;
            Vector2 raycastDirection = (player.transform.position - transform.position).normalized;
            Debug.DrawRay(transform.position, raycastDirection * rayDistance, Color.red); // Visualize the ray in the Scene view.

            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, rayDistance, noSeeThroughLayer);

            if (hit.collider != null)
            {

                if (hit.collider.gameObject == player)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    IEnumerator FireLaser()
    {
        yield return new WaitForSeconds(fireTimer);
        isCharging = false;
        StartCoroutine(ActuallyFireLaser());
    }

    IEnumerator ActuallyFireLaser()
    {
        yield return new WaitForSeconds(dodgeTimer);
        Debug.Log("Actually Fired Laser");
        Instantiate(laserParticle, firePoint);
        RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, directionToTarget, 1000f);

        Instantiate(explosionParticle, _hit.point, Quaternion.identity);

        if (_hit.collider.gameObject.tag == "Player" && _hit.collider != null) 
        {
            Debug.Log("Killed Bro");
            StartCoroutine(PlayerStatManager.instance.OnPlayerDie());
        }
        lineRenderer.enabled = false;
        StartCoroutine(ResetCanFire());
    }

    IEnumerator ResetCanFire()
    {
        yield return new WaitForSeconds(resetTimer);
        lineRenderer.enabled = true;
        canFire = true;
    }
}
