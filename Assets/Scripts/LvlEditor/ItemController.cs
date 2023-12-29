using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{
    public int ID;
    private LevelEditorManager editor;

    public bool buttonClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    public void OnButtonClick()
    {
        if (ID != 3)
        {
            Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            Instantiate(editor.ItemImage[ID], new Vector3(worldPos.x, worldPos.y, 0), Quaternion.identity);
        }
        buttonClicked = true;
        editor.currButtonPressed = ID;
    }
}
