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

    public int audioInt;
    public AudioClip[] audioClip;
    private string datapath;

    [Header("PlayerSaveData")]
    public int PlayerJob;
    public int LevelNumber;
    public int[] PlayerUpgrade = new int[3] {0,0,0};

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
        LordingSave();
    }
    // Start is called before the first frame update
    private void Start()
    {
        datapath = Application.dataPath + "/Save" + "/playersave.json";
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            LevelNumber = 2;
            PlayerJob = 0;
            PlayerUpgrade = new int[3] { 1, 1, 1 };
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }*/
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
    public void LordingSave()
    {
        datapath = Application.dataPath + "/Save" + "/playersave.json";
        if (File.Exists(datapath))
        {
            string playersave = File.ReadAllText(datapath);
            SaveData savedata = JsonConvert.DeserializeObject<SaveData>(playersave);
            LevelNumber = savedata.Level;
            PlayerUpgrade = savedata.PlayerUpgrade;
            Debug.Log("Lording Save");
        }
        else
        {
            CreateNewSave();
        }
    }
    private void CreateNewSave()
    {
        LevelNumber = 0;
        PlayerUpgrade = new int[3] { 0, 0, 0 };
        SaveData savedata = new SaveData();
        savedata.Level = LevelNumber;
        savedata.PlayerUpgrade = PlayerUpgrade;
        string savejson = JsonConvert.SerializeObject(savedata);
        File.WriteAllText(datapath, savejson);
        Debug.Log("Create New Save");
    }
    public void Save()
    {
        string playersave = File.ReadAllText(datapath);
        SaveData savedata = JsonConvert.DeserializeObject<SaveData>(playersave);
        savedata.Level = LevelNumber;
        savedata.PlayerUpgrade = PlayerUpgrade;
        savedata.PlayerJobID = PlayerUIManager.GetInstance().PlayerData.JobIndex;
        string savejson = JsonConvert.SerializeObject(savedata);
        File.WriteAllText(datapath, savejson);
        Debug.Log("Save");
    }
    public void StartNewGame()
    {
        PlayerJob = Random.Range(0, Jobs.Count);
        LevelNumber = 1;
        PlayerUpgrade = new int[3] { 0, 0, 0 };
        Debug.Log("New Game");
    }
}
public class SaveData
{
    public int Level;
    public int PlayerJobID;
    public int[] PlayerUpgrade;
}
