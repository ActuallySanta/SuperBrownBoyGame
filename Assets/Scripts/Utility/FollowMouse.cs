using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos = new Vector2(Mathf.Round(worldPos.x * 10f) / 10f, Mathf.Round(worldPos.y * 10f) / 10f);

        transform.position = new Vector3(worldPos.x,worldPos.y, 0);
    }
}
