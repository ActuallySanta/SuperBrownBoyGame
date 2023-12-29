using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using UnityEngine.Networking;
using TMPro;
using System.IO;

public class FileManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    public int currFileType;

    public void OnClickOpen(int _currFileType)
    {
        currFileType = _currFileType;
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open Tile and Item File","","json",true);
        for (int i = 0; i < paths.Length; i++) 
        {
            StartCoroutine(OutputRoutineOpen(new System.Uri(paths[i]).AbsoluteUri));
        }
    }

    private IEnumerator OutputRoutineOpen(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW ERROR: " + www.error);
        }
        else
        {
            string fileName = Path.GetFileName(www.url);
            
            if (fileName.Contains("level")) levelManager.tileJSON = www.downloadHandler.text;
            else if (fileName.Contains("item")) levelManager.itemJSON = www.downloadHandler.text;
            
            levelManager.LoadLevel();
        }
    }
}
