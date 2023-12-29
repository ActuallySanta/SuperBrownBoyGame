using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.IO;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public List<CustomTile> tiles = new List<CustomTile>();

    public bool playMode = false;

    [SerializeField] TMP_InputField nameSelector;

    [SerializeField] GameObject PlayerPrefab;

    [SerializeField] private Canvas EditModeCanvas;

    [SerializeField] private TMP_Text savedLevelText;
    [SerializeField] private float savedTextWaitTime = 2f;

    [SerializeField] private Slider camDistanceSlider;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float camMult = 5f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        foreach (Tilemap tilemap in tileMaps)
        {
            foreach (Tilemaps num in System.Enum.GetValues(typeof(Tilemaps)))
            {
                if (tilemap.name == num.ToString())
                {
                    if (!layers.ContainsKey((int)num)) layers.Add((int)num, tilemap);
                }
            }
        }
    }

    [SerializeField] List<Tilemap> tileMaps = new List<Tilemap>();
    public Dictionary<int, Tilemap> layers = new Dictionary<int, Tilemap>();

    LevelData levelData = new LevelData();

    public string tileJSON;
    public string itemJSON;

    public enum Tilemaps
    {
        Sky = 30,
        Background = 40,
        Level = 50,
        Foreground = 60,
    }

    private void Update()
    {

        if (nameSelector.isFocused) return;

        if (!playMode) CamDistanceSlider();

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A)) SaveLevel();
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S)) LoadLevel();

        if (Input.GetKeyDown(KeyCode.P) && !playMode) StartPlayMode();

        if(playMode && GameObject.FindGameObjectWithTag("Player")) PlayerStatManager.instance.isRunning = playMode;

        if (Input.GetKeyDown(KeyCode.M) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void SaveLevel()
    {
        LevelData levelData = new LevelData();
        ItemData itemData = new ItemData();

        foreach (var item in layers.Keys)
        {
            levelData.layers.Add(new LayerData(item));
        }

        foreach (var layerData in levelData.layers)
        {

            if (!layers.TryGetValue(layerData.layerID, out Tilemap tilemap)) break;


            BoundsInt bounds = tilemap.cellBounds;


            for (int x = bounds.min.x; x < bounds.max.x; x++)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    Debug.Log(layerData.layerID);
                    TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));
                    CustomTile tempTile = tiles.Find(t => t.tile == temp);

                    if (temp != null)
                    {
                        layerData.tiles.Add(tempTile.id);
                        layerData.positions.Add(new Vector3Int(x, y, 0));
                    }
                }
            }

        }
        GameObject[] items = GameObject.FindGameObjectsWithTag("LevelItem");
        Debug.Log(items.Length);
        for (int i = 0; i < items.Length; i++)
        {
            GameObject prefabGameObject = items[i].gameObject;
            Debug.Log(prefabGameObject.name);
            itemData.itemNames.Add(prefabGameObject.name);
            itemData.positions.Add(Vector3Int.FloorToInt(items[i].transform.position));
        }



        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/SSB Levels";

        if (!Directory.Exists(desktopPath))
        {
            Directory.CreateDirectory(desktopPath);

            Debug.Log("Folder created at: " + desktopPath);
        }


        // Specify the folder path
        string folderPath = Path.Combine(desktopPath, nameSelector.text);

        // Check if the folder doesn't exist
        if (!Directory.Exists(folderPath))
        {
            // Create the new folder
            Directory.CreateDirectory(folderPath);

            Debug.Log("Folder created at: " + folderPath);
        }
        else
        {
            Debug.LogWarning("Folder already exists at: " + folderPath);
        }

        string levelJson = JsonUtility.ToJson(levelData, true);
        string tileFilePath = Path.Combine(folderPath, nameSelector.text + " levelTiles.json");
        File.WriteAllText(tileFilePath, levelJson);

        string itemJson = JsonUtility.ToJson(itemData, true);
        string itemFilePath = Path.Combine(folderPath, nameSelector.text + " itemTiles.json");
        File.WriteAllText(itemFilePath, itemJson);

        if (!savedLevelText.isActiveAndEnabled)
        {
            savedLevelText.gameObject.SetActive(true);
        }

        savedLevelText.text = "Saved at: " + folderPath;

        StartCoroutine(SavedTextDeactivate());

    }

    public void LoadLevel()
    {
        LevelData levelData = JsonUtility.FromJson<LevelData>(tileJSON);
        ItemData itemData = JsonUtility.FromJson<ItemData>(itemJSON);

        foreach (var data in levelData.layers)
        {

            if (!layers.TryGetValue(data.layerID, out Tilemap tilemap)) break;

            tilemap.ClearAllTiles();

            for (int i = 0; i < data.tiles.Count; i++)
            {
                TileBase tile = tiles.Find(t => t.id == data.tiles[i]).tile;
                if (tile) tilemap.SetTile(data.positions[i], tiles.Find(t => t.name == data.tiles[i]).tile);
            }
        }

        foreach (var data in itemData.itemNames)
        {
            if (data.Count() == 0) break;

            for (int i = 0; i <= data.Count(); i++)
            {
                Debug.Log(itemData.itemNames[i]);
                GameObject _item = Resources.Load<GameObject>(itemData.itemNames[i].ToString());
                if (_item != null) Instantiate(_item, itemData.positions[i], Quaternion.identity);
                else Debug.Log("NO ITEM FOUND");
            }
        }

        Debug.Log("LOADED LEVEL");
    }


    void StartPlayMode()
    {
        playMode = true;
        EditModeCanvas.gameObject.SetActive(false);
        GameObject respawnPoint = GameObject.Find("RespawnPoint");
        if (respawnPoint == null) respawnPoint = GameObject.Find("RespawnPoint(Clone)");

        Instantiate(PlayerPrefab, respawnPoint.transform.position, Quaternion.identity);
    }

    public void EndPlayMode()
    {
        playMode = false;
        EditModeCanvas.gameObject.SetActive(true);
    }

    IEnumerator SavedTextDeactivate()
    {
        yield return new WaitForSeconds(savedTextWaitTime);

        savedLevelText.gameObject.SetActive(false);
    }

    void CamDistanceSlider()
    {
        cam.m_Lens.OrthographicSize = camDistanceSlider.value * camMult;
    }
}


[System.Serializable]
public class LevelData
{
    public List<LayerData> layers = new List<LayerData>();
}

[System.Serializable]
public class LayerData
{
    public int layerID;
    public List<string> tiles = new List<string>();
    public List<Vector3Int> positions = new List<Vector3Int>();

    public LayerData(int id)
    {
        layerID = id;
    }
}

[System.Serializable]
public class ItemData
{
    public List<string> itemNames = new List<string>();
    public List<Vector3Int> positions = new List<Vector3Int>();
}

