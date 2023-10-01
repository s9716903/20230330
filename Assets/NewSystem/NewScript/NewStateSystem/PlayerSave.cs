using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
public class PlayerSave : MonoBehaviour
{
    private string datapath;

    public int StoryChapterUnlock;
    private int[] PlayerJobUnlock;
    public List<int[]> JobDataUnlock = new List<int[]>();
    // Start is called before the first frame update
    private void Start()
    {
        datapath = Application.dataPath + "/Save" + "/playersave.json";
        if (File.Exists(datapath))
        {
            string playersave = File.ReadAllText(datapath);
            SaveData savedata = JsonConvert.DeserializeObject<SaveData>(playersave);
            StoryChapterUnlock = savedata.StoryChapterUnlock;
            JobDataUnlock = savedata.JobData;
        }
        else
        {
            CreateNewSave();
            NewSaveData();
        }
    }
    private void CreateNewSave()
    {
        for (int i = 0; i < NewPlayerSkillManager.Jobs.Count; i++)
        {
            PlayerJobUnlock = new int[] { i, 0, 0 };
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
    }
}
public class SaveData
{
    public int StoryChapterUnlock;
    public List<int[]> JobData;
}
