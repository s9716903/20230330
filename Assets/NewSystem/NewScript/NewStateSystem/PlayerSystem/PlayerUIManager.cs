using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerUIManager : Singleton<PlayerUIManager>
{
    public GameObject PlayerPiece;
    public GameObject PlayerPieceLocation;
    public GameObject ResultButton;

    public int HandCardAmount;

    public bool readyToDuel;
    public bool isFirstATK;
    
    public GameObject PlayerHandCardZone;
    public GameObject PlayerDeck;
    //public GameObject PlayerTrashCardZone;
    public GameObject PlayerState;
    public PlayerDataManager PlayerData;
    
    public GameObject PlayerPrefab;
    public GameObject CardPrefab;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        PlayerData = new PlayerDataManager();
        PlayerData.MoveToLocation[0] = 1;
        PlayerData.MoveToLocation[1] = 1;
        PlayerData.SettingValue();
        //PracticeLimited = false;
        PlayerHandCardZone.SetActive(false);
        PlayerDeck.SetActive(false);
        ResultButton.SetActive(false);
        //PlayerTrashCardZone.SetActive(false);
        PlayerState.SetActive(false);
        StartCoroutine(StartDuel());
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerData.HP = PlayerHandCardZone
        if (PlayerData.isReady && (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move || DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack))
        {
            PlayerData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerReady;
        }
        HandCardAmount = PlayerHandCardZone.transform.childCount;
    }
    public void MovePiece()
    {
        PlayerPiece.transform.parent = PlayerPieceLocation.transform.GetChild(PlayerData.MoveToLocation[0]).transform.GetChild(PlayerData.MoveToLocation[1]);
        PlayerPiece.transform.DOMove(PlayerPieceLocation.transform.GetChild(PlayerData.MoveToLocation[0]).transform.GetChild(PlayerData.MoveToLocation[1]).transform.position, 1);
        PlayerData.PlayerLocation = PlayerData.MoveToLocation;
    }
    public void PlayerAttackReady()
    {
        PlayerData.isReady = true;
    }
    public virtual IEnumerator AttackReady()
    {
        var targetlist = new List<Transform>();
        for (int i = 0; i < PlayerHandCardZone.transform.childCount; i++)
        {
            var targetcard = PlayerHandCardZone.transform.GetChild(i);
            if (targetcard.GetComponent<NewCardValueManager>().usecard == true)
            {
                targetcard.GetComponent<NewCardValueManager>().resultCard = true;
                targetlist.Add(targetcard);
                while (targetcard.transform.position.x != PlayerPiece.transform.position.x && targetcard.transform.position.y != PlayerPiece.transform.position.y)
                {
                    targetcard.transform.position = Vector2.MoveTowards(targetcard.transform.position, PlayerPiece.transform.position, 1500 * Time.deltaTime);
                    yield return 0;
                }
                targetcard.gameObject.SetActive(false);
            }
        }
        for (int j = 0; j < targetlist.Count; j++)
        {
            Destroy(targetlist[j].gameObject);
        }
        targetlist.Clear();
        yield return null;
    }
    public void NormalDrawCard()
    {
        StartCoroutine(NormalDraw());
    }
    public virtual IEnumerator NormalDraw()
    {
        if (PlayerHandCardZone.transform.childCount >= 7)
        {
            DuelUIManager.showInformationText = true;
            DuelUIManager.Information = "Upper Limited";
            yield return new WaitForSeconds(1f);
            DuelUIManager.showInformationText = false;
            PlayerData.isReady = true;
        }
        else
        {
            for (int i = 0; i < PlayerData.DrawAmount; i++)
            {
                var newcard = Instantiate(CardPrefab, PlayerDeck.transform);
                while (newcard.transform.position != PlayerHandCardZone.transform.position)
                {
                    newcard.transform.position = Vector2.MoveTowards(newcard.transform.position, PlayerHandCardZone.transform.position, 3500 * Time.deltaTime);
                    yield return 0;
                }
                newcard.transform.parent = PlayerHandCardZone.transform; //�d���ܦ���P�l����
                PlayerHandCardZone.transform.GetChild(index: PlayerHandCardZone.transform.childCount - 1).GetComponent<NewCardTurnTopOrBottom>().CardStartTop();
                yield return 0;
            }
            if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Draw)
            {
                yield return new WaitForSeconds(1f);
                PlayerData.isReady = true;
            }
        }
        PlayerData.DrawAmount = 0;
        yield return 0;
    }
    public virtual IEnumerator StartDuel()
    {
        yield return new WaitForSeconds(1);
        PlayerDeck.SetActive(true);
        //PlayerTrashCardZone.SetActive(true);
        PlayerState.SetActive(true);
        PlayerHandCardZone.SetActive(true);
        yield return new WaitForSeconds(1);
        ResultButton.SetActive(true);
        PlayerPiece = Instantiate(PlayerPrefab, PlayerPieceLocation.transform.GetChild(PlayerData.MoveToLocation[0]).transform.GetChild(PlayerData.MoveToLocation[1])); 
        PlayerData.PlayerLocation = PlayerData.MoveToLocation;
        PlayerData.DrawAmount = 5;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(NormalDraw());
        readyToDuel = true;
        yield return 0;
    }
}
