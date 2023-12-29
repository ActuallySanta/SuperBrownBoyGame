using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerStatManager : MonoBehaviour
{
    public GameObject playerObject;

    [Header("Death Vars")]
    public float restarts;
    public Vector3 respawnPoint;

    [Header("Timer Vars")]
    public float startTime;
    public float stopTime;
    public float personalBestTime;

    [Header("Scene Changes")]
    public float sceneChangeTime = 1f;

    public bool isRunning = true;

    public static PlayerStatManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isRunning = true;
        GameObject[] rspPoints;
        rspPoints = GameObject.FindGameObjectsWithTag("LevelItem");

        for (int i = 0; i < rspPoints.Length; i++)
        {
            if (rspPoints[i].name.Contains("RespawnPoint"))
            {
                respawnPoint = rspPoints[i].transform.position;
                break;
            }
        }
    }


    public void SetPersonalBest()
    {
        
    }

    private void OnSceneChange(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }

        startTime = Time.time;
    }

    public IEnumerator OnWin(string _scene)
    {
        isRunning = false;
        yield return new WaitForSeconds(sceneChangeTime);
        if (_scene != "")
        {
            SceneManager.LoadScene(_scene);
            gameObject.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            LevelManager.Instance.EndPlayMode();
            isRunning = false;
            Destroy(gameObject);
        }
        else { Debug.Log("No Scene Selected"); }
        
    }

    public IEnumerator OnPlayerDie()
    {
        isRunning = false;
        restarts++;
        playerObject.transform.position = respawnPoint;
        startTime = Time.time;
        yield return new WaitForEndOfFrame();
        isRunning = true;
    }
}
