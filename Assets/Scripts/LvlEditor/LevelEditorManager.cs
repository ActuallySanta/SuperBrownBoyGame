using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelEditorManager : MonoBehaviour
{
    public ItemController[] ItemButtons;
    public GameObject[] ItemPrefabs;

    public GameObject[] ItemImage;

    public int currButtonPressed;

    [SerializeField] LevelEditor editor;

    private void Update()
    {
        GameObject image = GameObject.FindGameObjectWithTag("ItemImage");

        if (Input.GetKeyDown(KeyCode.R) && image != null)
        {
            image.transform.localEulerAngles = new Vector3(image.transform.localEulerAngles.x, image.transform.localEulerAngles.y, image.transform.localEulerAngles.z + 90);
        }

        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        worldPos = new Vector2(Mathf.Round(worldPos.x * 10f) / 10f, Mathf.Round(worldPos.y * 10f) / 10f);
        
        if (Input.GetMouseButtonDown(0) && ItemButtons[currButtonPressed].buttonClicked && currButtonPressed != 3)
        {
            ItemButtons[currButtonPressed].buttonClicked = false;

            GameObject item = Instantiate(ItemPrefabs[currButtonPressed], new Vector3(worldPos.x,worldPos.y, 0), image.transform.rotation);

            item.name = ItemPrefabs[currButtonPressed].name;

            Destroy(image);
        }

        if (ItemButtons[currButtonPressed].buttonClicked && currButtonPressed == 3)
        {
            //Tiles are being placed
            editor.isSettingTiles = true;
        }
        else
        {
            editor.isSettingTiles = false;
        }

    }
}
