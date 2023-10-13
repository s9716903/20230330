using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyManager : Singleton<EnemyManager>
{
    public GameObject EnemyPrefab; 
    public List<GameObject> EnemyPiece; //場景中敵人
    public GameObject EnemyPieceLocation; //敵人區域

    public bool readyToDuel;
    public bool isReady;
    public bool isFirstATK;

    public int HowManyEnemys; //場景中生成敵人數

    private List<int[]> RandomList;
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        isReady = true;
        HowManyEnemys = 3;
        RandomList = new List<int[]>();
        ResetRandomList();
        //EnemyPiece.SetActive(false);
        StartCoroutine(StartDuel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetRandomList()
    {
        RandomList.Clear();
        for (int i = 3; i < 6; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                RandomList.Add(new int[] { i, j });
            }
        }
    }
    /*public void MovePiece()
    {
        for (int i = 0; i < EnemyPiece.Length; i++)
        {
            EnemyPiece[i].transform.DOMove(EnemyPieceLocation.transform.GetChild(EnemyPiece[i].GetComponent<EnemyData>().MoveToLocation[0] - 2).transform.GetChild(EnemyPiece[i].GetComponent<EnemyData>().MoveToLocation[1]).transform.position, 1);
            EnemyPiece[i].GetComponent<EnemyData>().ThisLocation = EnemyPiece[i].GetComponent<EnemyData>().MoveToLocation;
        }
    }*/
    public IEnumerator StartDuel()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < HowManyEnemys; i++)
        {
            var enemylocation = Random.Range(0,RandomList.Count);
            var _enemyPrefab = Instantiate(EnemyPrefab, EnemyPieceLocation.transform);
            _enemyPrefab.GetComponent<EnemyData>().PlayerLocation = RandomList[enemylocation];
            //_enemyPrefab.GetComponent<EnemyData>().MoveToLocation = RandomList[enemylocation];
            RandomList.Remove(RandomList[enemylocation]);
            _enemyPrefab.transform.parent = EnemyPieceLocation.transform.GetChild(_enemyPrefab.GetComponent<EnemyData>().PlayerLocation[0] - 3).transform.GetChild(_enemyPrefab.GetComponent<EnemyData>().PlayerLocation[1]);
            _enemyPrefab.transform.position = EnemyPieceLocation.transform.GetChild(_enemyPrefab.GetComponent<EnemyData>().PlayerLocation[0] - 3).transform.GetChild(_enemyPrefab.GetComponent<EnemyData>().PlayerLocation[1]).transform.position;
            _enemyPrefab.GetComponent<EnemyData>().PlayerLocation = _enemyPrefab.GetComponent<EnemyData>().PlayerLocation;
            EnemyPiece.Add(_enemyPrefab);
            //_enemyPrefab.transform.position = EnemyPieceLocation.transform.GetChild(_enemyPrefab.GetComponent<EnemyData>().MoveToLocation[0] - 2).transform.GetChild(_enemyPrefab.GetComponent<EnemyData>().MoveToLocation[1]).transform.position;
            //_enemyPrefab.GetComponent<EnemyData>().PlayerLocation = _enemyPrefab.GetComponent<EnemyData>().MoveToLocation;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        readyToDuel = true;
    }
}
