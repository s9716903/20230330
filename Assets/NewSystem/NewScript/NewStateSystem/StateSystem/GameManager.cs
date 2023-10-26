using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager_instance = null;

    public Dictionary<int, JobData> Jobs = new Dictionary<int, JobData>();
    public Dictionary<int, EnemyType> Enemys = new Dictionary<int, EnemyType>();

    private string datapath;
    public int LevelNumber;

    // Start is called before the first frame update
    private void Awake()
    {
        if (gameManager_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        gameManager_instance = this;
        DontDestroyOnLoad(gameObject);
        InitializePlayerJob();
        InitializeEnemy();
    }
    // Start is called before the first frame update
    private void Start()
    {
        datapath = Application.dataPath + "/Save" + "/playersave.json";
        if (File.Exists(datapath))
        {
            string playersave = File.ReadAllText(datapath);
            SaveData savedata = JsonConvert.DeserializeObject<SaveData>(playersave);
            LevelNumber = savedata.Level;
            Debug.Log("Lording Save");
        }
        else
        {
            CreateNewSave();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LevelNumber = 2;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }
    private void InitializePlayerJob()
    {
        Jobs.Clear();
        Jobs.Add(0, new Warrior());
        Jobs.Add(1, new Magician());
    }
    private void InitializeEnemy()
    {
        Enemys.Clear();
        Enemys.Add(0, new Tomato());
    }
    private void CreateNewSave()
    {
        SaveData savedata = new SaveData();
        string savejson = JsonConvert.SerializeObject(savedata);
        File.WriteAllText(datapath, savejson);
        Debug.Log("Create New Save");
    }
    private void Save()
    {
        string playersave = File.ReadAllText(datapath);
        SaveData savedata = JsonConvert.DeserializeObject<SaveData>(playersave);
        savedata.Level = LevelNumber;
        savedata.PlayerJobID = PlayerUIManager.GetInstance().PlayerData.JobIndex;
        string savejson = JsonConvert.SerializeObject(savedata);
        File.WriteAllText(datapath, savejson);
        Debug.Log("Save");
    }
    public void StartNewGame()
    { 
        for (int i = 0; i < Jobs.Count; i++)
        {
            Jobs[i].Setting();
        }
        for (int j = 0; j < Enemys.Count; j++)
        {
            Jobs[j].Setting();
        }
    }
}
public class SaveData
{
    public int Level;
    public int PlayerJobID;
}
