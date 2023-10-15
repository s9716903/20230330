using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager_instance = null;

    public Dictionary<int, JobData> Jobs = new Dictionary<int, JobData>();
    public List<int> PlayerJob;
    //public TextAsset PlayerInformationAsset;
    //public TextAsset EnemyInformationAsset;

    private string datapath;
    private int JobUsingAchievement;
    public List<int> AchievementList = new List<int>();

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
        InitializeJob();
    }
    // Start is called before the first frame update
    private void Start()
    {
        ClearPlayerJob();
        datapath = Application.dataPath + "/Save" + "/playersave.json";
        if (File.Exists(datapath))
        {
            string playersave = File.ReadAllText(datapath);
            SaveData savedata = JsonConvert.DeserializeObject<SaveData>(playersave);
            AchievementList = savedata.JobData;
            Debug.Log("Lording Save");
        }
        else
        {
            CreateNewSave();
            NewSaveData();
        }
    }
    private void InitializeJob()
    {
        Jobs.Clear();
        Jobs.Add(0, new NoSkill());
        Jobs.Add(1, new SkillTest());
        Jobs.Add(2, new SkillTest2());
        for (int i = 0; i < Jobs.Count; i++)
        {
            Jobs[i].Setting();
        }
    }
    private void CreateNewSave()
    {
        for (int i = 0; i < Jobs.Count; i++)
        {
            JobUsingAchievement = 0;
            AchievementList.Add(JobUsingAchievement);
        }
    }
    private void NewSaveData()
    {
        SaveData savedata = new SaveData();
        savedata.JobData = AchievementList;
        string savejson = JsonConvert.SerializeObject(savedata);
        File.WriteAllText(datapath, savejson);
        Debug.Log("Create New Save");
    }
    public void ClearPlayerJob()
    {
        if (PlayerJob != null)
        {
            PlayerJob.Clear();
        }
    }
}
public class SaveData
{
    public List<int> JobData;
}
