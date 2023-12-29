using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleCannon : DamagingObject
{
    public float fireTimer, rayDistance, lookLerpSpeed, resetTimer;
    public LayerMask playerLayer, noSeeThroughLayer;

    public Transform missleCannon;
    public Transform firePoint;
    
    GameObject player;
    public GameObject homingMisslePrefab, standardMisslePrefab;
    
    bool canSeePlayer;
    bool canFire;

    enum MissleType
    {
        standard,
        homing,
    }

    enum TrackingType
    {
        tracking,
        noTracking,
    }

    [SerializeField] MissleType currType;
    [SerializeField] TrackingType currTrackingType;


    public override void Start()
    {
        base.Start();
        canFire = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        bool canSeePlayer = CheckForPlayer();
        if (player != null && canSeePlayer)
        {
            if (canFire)
            {
                canFire = false;
                StartCoroutine(ShootMissleAtPlayer());
            }

            if (currTrackingType == TrackingType.tracking)
            {
                // Get the direction to the target object
                Vector2 directionToTarget = (player.transform.position - transform.position);

                missleCannon.transform.up = Vector2.Lerp(missleCannon.transform.up, directionToTarget, lookLerpSpeed);
            }
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

    IEnumerator ShootMissleAtPlayer()
    {
        Debug.Log("shooted at lad");
        yield return new WaitForSeconds(fireTimer);
        GameObject missle;
        if (currType == MissleType.homing)
        {
            missle = Instantiate(homingMisslePrefab, firePoint);
        }
        else
        {
            missle = Instantiate(standardMisslePrefab, firePoint);
        }
        missle.transform.parent = null;
        StartCoroutine(ResetCanFire());
    }

    IEnumerator ResetCanFire()
    {
        yield return new WaitForSeconds(resetTimer);
        canFire = true;
    }
}
