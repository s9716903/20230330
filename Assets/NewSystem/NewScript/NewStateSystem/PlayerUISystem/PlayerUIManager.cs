using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerUIManager : Singleton<PlayerUIManager>
{
    public GameObject PlayerPiece;
    public GameObject PlayerPieceLocation;

    public int HandCardAmount;

    public bool readyToDuel;
    public bool isReady;
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
        PlayerData.MoveToLocation[0] = 0;
        PlayerData.MoveToLocation[1] = 0;
        PlayerData.SettingValue();
        //PracticeLimited = false;
        PlayerHandCardZone.SetActive(false);
        PlayerDeck.SetActive(false);
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
    public void PlayerReady()
    {    
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack)
        {
            for (int i = 0; i < PlayerHandCardZone.transform.childCount; i++)
            {
                var targetcard = PlayerHandCardZone.transform.GetChild(i);
                if (targetcard.GetComponent<NewCardValueManager>().usecard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 10, 0);
                }
            }
            for (int j = 0; j < PlayerHandCardZone.transform.childCount; j++)
            {
                var targetcard = PlayerHandCardZone.transform.GetChild(j);
                if (targetcard.GetComponent<NewCardValueManager>().usecard == false)
                {
                    targetcard.gameObject.SetActive(false);
                }
            }
        }
    }

    /*public void ShowHandCard()
    {
        for (int i = 0; i < PlayerHandCardZone.transform.childCount; i++)
        {
            if (PlayerHandCardZone.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                Destroy(PlayerHandCardZone.transform.GetChild(i).gameObject);
                //ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.Add(HandAllCard[i]);//手牌加入棄牌List
                //HandAllCard[i] = null; //牌組List中移除卡牌
            }
            else if (!PlayerHandCardZone.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                PlayerHandCardZone.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        //HandAllCard.RemoveAll(HandAllCard => HandAllCard == null);
    }*/

    public void NormalDrawCard()
    {
        StartCoroutine(NormalDraw());
    }
    public virtual IEnumerator NormalDraw()
    {
        for (int i = 0; i < PlayerData.DrawAmount; i++)
        {
            var newcard = Instantiate(CardPrefab, PlayerDeck.transform);
            while (newcard.transform.position != PlayerHandCardZone.transform.position)
            {
                newcard.transform.position = Vector2.MoveTowards(newcard.transform.position, PlayerHandCardZone.transform.position, 3000 * Time.deltaTime);
                yield return 0;
            }
            newcard.transform.parent = PlayerHandCardZone.transform; //卡片變成手牌子物件
            PlayerHandCardZone.transform.GetChild(index: PlayerHandCardZone.transform.childCount - 1).GetComponent<NewCardTurnTopOrBottom>().CardStartTop();
            yield return 0;
        }
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Draw)
        {
            yield return new WaitForSeconds(1f);
            PlayerData.isReady = true;
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
        PlayerPiece = Instantiate(PlayerPrefab, PlayerPieceLocation.transform.GetChild(PlayerData.MoveToLocation[0]).transform.GetChild(PlayerData.MoveToLocation[1])); 
        PlayerData.PlayerLocation = PlayerData.MoveToLocation;
        PlayerData.DrawAmount = PlayerData.StartCard;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(NormalDraw());
        readyToDuel = true;
        yield return 0;
    }
}
