using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour
{
    public void LevelSelect(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
