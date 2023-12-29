using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryLevelManager : MonoBehaviour
{
    public bool isPlaying = false;
    public GameObject playerObj;
    public Transform respawnPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlaying) 
        {
            StartLevel();
        }
    }

    void StartLevel()
    {
        isPlaying = true;
        if (PlayerStatManager.instance == null)
        {
            Instantiate(playerObj,respawnPoint.position,Quaternion.identity);
        }
        else
        {
            PlayerStatManager.instance.gameObject.SetActive(true);
            PlayerStatManager.instance.gameObject.transform.position = respawnPoint.position;
            PlayerStatManager.instance.respawnPoint = respawnPoint.position;
            PlayerStatManager.instance.isRunning = false;
            PlayerStatManager.instance.isRunning = true;
        }
    }
}
