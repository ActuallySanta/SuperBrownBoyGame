using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnToMenu : MonoBehaviour
{
    public static ReturnToMenu instance;

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

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ReturnToMainMenu());
        }
    }

    IEnumerator ReturnToMainMenu()
    {
        if (PlayerStatManager.instance != null) PlayerStatManager.instance.isRunning = false;
        yield return new WaitForEndOfFrame();
        if (PlayerStatManager.instance != null) PlayerStatManager.instance.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
