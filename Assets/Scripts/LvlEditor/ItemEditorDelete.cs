using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ItemEditorDelete : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (LevelManager.Instance == null) return;
        if (Input.GetMouseButtonDown(1) && !LevelManager.Instance.playMode)
        {
            Destroy(this.gameObject);
        }
    }
}
