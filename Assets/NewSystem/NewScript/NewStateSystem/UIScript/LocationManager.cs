using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationManager : MonoBehaviour
{
    //public static int[] PlayerChooseMoveCoordinates;
    //public static int[] EnemyChooseMoveCoordinates;
    private List<List<GameObject>> AllLocations;
    private List<List<GameObject>> PlayerLocations;
    private List<List<GameObject>> EnemyLocations;
    private List<GameObject> Locations;
    public GameObject PlayerPieceLocation;
    public GameObject EnemyPieceLocation;
    public PlayerDataManager _playerData;
    public PlayerDataManager _enemyData;

    // Start is called before the first frame update
    void Start()
    {
        AllLocations = new List<List<GameObject>>();
        PlayerLocations = new List<List<GameObject>>();
        EnemyLocations = new List<List<GameObject>>();
        for (int i = 0; i < PlayerPieceLocation.transform.childCount; i++)
        {
            Locations = new List<GameObject>();
            for (int j = 0; j < PlayerPieceLocation.transform.GetChild(i).childCount; j++)
            {
                Locations.Add(PlayerPieceLocation.transform.GetChild(i).transform.GetChild(j).gameObject);
            }
            PlayerLocations.Add(Locations);
            AllLocations.Add(Locations);
        }
        for (int k = 0; k < EnemyPieceLocation.transform.childCount; k++)
        {
            Locations = new List<GameObject>();
            for (int l = 0; l < EnemyPieceLocation.transform.GetChild(k).childCount; l++)
            {
                Locations.Add(EnemyPieceLocation.transform.GetChild(k).transform.GetChild(l).gameObject);
            }
            EnemyLocations.Add(Locations);
            AllLocations.Add(Locations);
        }
        for (int a = 0; a < AllLocations.Count; a++)
        {
            for (int b = 0; b < AllLocations[a].Count; b++)
            {
                AllLocations[a][b].GetComponent<CoordinatesLocation>().Coordinates[0] = a;
                AllLocations[a][b].GetComponent<CoordinatesLocation>().Coordinates[1] = b;
            }
        }
    }
    private void Update()
    {
        _playerData = PlayerUIManager.GetInstance().PlayerData;
        _enemyData = EnemyUIManager.GetInstance().EnemyData;
        for (int i = 0; i < PlayerLocations.Count; i++)
        {
            for (int j = 0; j < PlayerLocations[i].Count; j++)
            {
                PlayerLocations[i][j].GetComponent<CoordinatesLocation>().playerData = _playerData;
            }
        }
        for (int i = 0; i < EnemyLocations.Count; i++)
        {
            for (int j = 0; j < EnemyLocations[i].Count; j++)
            {
                EnemyLocations[i][j].GetComponent<CoordinatesLocation>().playerData = _enemyData;
            }
        }
    }
}
