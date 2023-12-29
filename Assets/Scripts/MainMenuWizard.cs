using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuWizard : MonoBehaviour
{
    [SerializeField] GameObject mainMenuParent, settingsMenuParent, helpMenuParent,levelSelectParent;

    public void OpenMainMenu()
    {
        GameObject[] openMenus = GameObject.FindGameObjectsWithTag("Menu");

        for (int i = 0; i < openMenus.Length; i++) 
        {
            openMenus[i].SetActive(false);
        }
            mainMenuParent.SetActive(true);
    }

    public void OpenHelpMenu()
    {
        GameObject[] openMenus = GameObject.FindGameObjectsWithTag("Menu");

        for (int i = 0; i < openMenus.Length; i++)
        {
            openMenus[i].SetActive(false);
        }
            helpMenuParent.SetActive(true);
    }

    public void OpenSettingsMenu()
    {
        GameObject[] openMenus = GameObject.FindGameObjectsWithTag("Menu");

        for (int i = 0; i < openMenus.Length; i++)
        {
            openMenus[i].SetActive(false);
        }
            settingsMenuParent.SetActive(true);
    }

    public void OpenLevelSelectMenu()
    {
        GameObject[] openMenus = GameObject.FindGameObjectsWithTag("Menu");

        for (int i = 0; i < openMenus.Length; i++)
        {
            openMenus[i].SetActive(false);
        }
        levelSelectParent.SetActive(true);
    }

    public void OpenLevelEditor()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenLevel(int _buildIndex)
    {
        SceneManager.LoadScene(_buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
