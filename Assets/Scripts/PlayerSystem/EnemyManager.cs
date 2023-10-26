using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class EnemyManager : Singleton<EnemyManager>
{
    public GameObject EnemyPrefab; 
    public List<GameObject> EnemyPiece; //場景中敵人
    public GameObject EnemyPieceLocation; //敵人區域
    public List<int[]> EnemyATKZone;
    public bool isEnemyDie;

    public bool readyToDuel;
    public bool isReady;

    public int HowManyEnemys; //場景中生成敵人數

    private List<int[]> RandomList;
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        isEnemyDie = false;
        isReady = true;
        HowManyEnemys = 3;
        RandomList = new List<int[]>();
        ResetRandomList();
        StartCoroutine(StartDuel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetRandomList()
    {
        RandomList.Clear();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                RandomList.Add(new int[] { i, j });
            }
        }
    }
    /*public void MovePiece()
    {
        for (int i = 0; i < EnemyPiece.Count; i++)
        {
            var MoveZone = EnemyPiece[i].GetComponent<EnemyData>().ActionZone[0];
            var MoveValue = Random.Range(0,MoveZone.Length);
        }
    }*/
    public IEnumerator EnemysHurt()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        for (int i = 0; i < EnemyPiece.Count; i++)
        {
            var hurt = _player_data.ATKHitZone.Any(arr => arr.GetType() == EnemyPiece[i].GetComponent<EnemyData>().EnemyLocation.GetType() && arr.SequenceEqual(EnemyPiece[i].GetComponent<EnemyData>().EnemyLocation));
            if (hurt)
            {
                EnemyPiece[i].GetComponent<EnemyData>().HP -= _player_data.ATKValue;
            }
        }
        EnemyPiece.RemoveAll(x => x.GetComponent<EnemyData>().HP <= 0);
        isEnemyDie = true;
        yield return new WaitForSeconds(1f);
        if (EnemyPiece.Count == 0)
        {
            DuelUIManager.Information = "Player Win";
            yield return new WaitForSeconds(1f);
            DuelUIManager.BattleEnd = true;
            yield break;
        }
    }
    public IEnumerator StartDuel()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < HowManyEnemys; i++)
        {
            var enemylocation = Random.Range(0,RandomList.Count);
            var _enemyPrefab = Instantiate(EnemyPrefab, EnemyPieceLocation.transform);
            _enemyPrefab.GetComponent<EnemyData>().EnemyLocation = RandomList[enemylocation];
            RandomList.Remove(RandomList[enemylocation]);
            _enemyPrefab.transform.parent = EnemyPieceLocation.transform.GetChild(_enemyPrefab.GetComponent<EnemyData>().EnemyLocation[0]).transform.GetChild(_enemyPrefab.GetComponent<EnemyData>().EnemyLocation[1]);
            _enemyPrefab.transform.position = EnemyPieceLocation.transform.GetChild(_enemyPrefab.GetComponent<EnemyData>().EnemyLocation[0]).transform.GetChild(_enemyPrefab.GetComponent<EnemyData>().EnemyLocation[1]).transform.position;
            //_enemyPrefab.GetComponent<EnemyData>().EnemyLocation = _enemyPrefab.GetComponent<EnemyData>().EnemyLocation;
            EnemyPiece.Add(_enemyPrefab);
            //_enemyPrefab.transform.position = EnemyPieceLocation.transform.GetChild(_enemyPrefab.GetComponent<EnemyData>().MoveToLocation[0] - 2).transform.GetChild(_enemyPrefab.GetComponent<EnemyData>().MoveToLocation[1]).transform.position;
            //_enemyPrefab.GetComponent<EnemyData>().PlayerLocation = _enemyPrefab.GetComponent<EnemyData>().MoveToLocation;
            yield return null;
        }
        readyToDuel = true;
    }
}
