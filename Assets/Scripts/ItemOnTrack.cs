using System.Collections;
using System;
using UnityEngine;

public class ItemOnTrack : MonoBehaviour
{
    [SerializeField] Transform[] followPoints;

    Transform originalFirstFollowPoint;

    [SerializeField] float moveSpeed;

    [SerializeField] Transform followerObject;

    int pointInd;

    public enum TrackType
    {
        oneWay,
        loopRoute,
        reverseAtEnd,
    }

    public enum WaitTime
    {
        hasWaitTime,
        hasWaitTimeAtStart,
        noWaitTime,
    }

    [SerializeField] TrackType currTrackType;
    [SerializeField] WaitTime currWaitTime;

    [SerializeField] float waitTime;
    bool isWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        originalFirstFollowPoint = followPoints[0];
        followerObject.position = followPoints[pointInd].transform.position;
        if (currWaitTime == WaitTime.hasWaitTime || currWaitTime == WaitTime.hasWaitTimeAtStart)
        {
            isWaiting = true;
            StartCoroutine(DoWaitTime(waitTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStatManager.instance == null || PlayerStatManager.instance.isRunning == false)
        {
            if (followPoints[0] != originalFirstFollowPoint)
            {
                Array.Reverse(followPoints);
                pointInd = 0;
            }

            followerObject.position = followPoints[0].transform.position;
            
            if (currWaitTime == WaitTime.hasWaitTime || currWaitTime == WaitTime.hasWaitTimeAtStart)
            {
                isWaiting = true;
                StartCoroutine(DoWaitTime(waitTime));
            }

            return;
        }

        if (isWaiting) return;
        
            if (pointInd <= followPoints.Length - 1)
            {
                followerObject.transform.position = Vector2.MoveTowards(followerObject.transform.position, followPoints[pointInd].transform.position, moveSpeed * Time.deltaTime);

                if (followerObject.transform.position == followPoints[pointInd].transform.position)
                {
                    pointInd++;

                    if (currWaitTime == WaitTime.hasWaitTime)
                    {
                        isWaiting = true;

                        StartCoroutine(DoWaitTime(waitTime));
                    }
                }
                if (pointInd == followPoints.Length)
                {
                    if (currTrackType == TrackType.reverseAtEnd)
                    {
                        Array.Reverse(followPoints);
                        pointInd = 0;
                    }

                    if (currTrackType == TrackType.loopRoute)
                    {
                        pointInd = 0;
                    }
                }
            }
        
    }

    IEnumerator DoWaitTime(float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);
        Debug.Log("No Longer Waiting");
        isWaiting = false; 
    }
}
