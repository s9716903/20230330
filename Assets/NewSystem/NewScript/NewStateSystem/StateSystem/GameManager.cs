using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager_instance = null;

    public Dictionary<int, JobData> Jobs = new Dictionary<int, JobData>();
    public int PlayerStoryChapterUnlock;
    public int[] PlayerJob;
    public int[] EnemyJob;
    public TextAsset PlayerDeckAsset;
    public TextAsset EnemyDeckAsset;

    private string datapath;

    public List<int[]> JobDataUnlock = new List<int[]>();

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
        datapath = Application.dataPath + "/Save" + "/playersave.json";
        if (File.Exists(datapath))
        {
            string playersave = File.ReadAllText(datapath);
            SaveData savedata = JsonConvert.DeserializeObject<SaveData>(playersave);
            PlayerStoryChapterUnlock = savedata.StoryChapterUnlock;
            JobDataUnlock = savedata.JobData;
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
        for (int i = 0; i < Jobs.Count; i++)
        {
            Jobs[i].Setting();
        }
    }
    private void CreateNewSave()
    {
        for (int i = 0; i < NewPlayerSkillManager.Jobs.Count; i++)
        {
            int[] PlayerJobUnlock = new int[] { i, 0, 0 };
            JobDataUnlock.Add(PlayerJobUnlock);
        }
    }
    private void NewSaveData()
    {
        SaveData savedata = new SaveData();
        savedata.StoryChapterUnlock = 0;
        savedata.JobData = JobDataUnlock;
        string savejson = JsonConvert.SerializeObject(savedata);
        File.WriteAllText(datapath, savejson);
        Debug.Log("Create New Save");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
