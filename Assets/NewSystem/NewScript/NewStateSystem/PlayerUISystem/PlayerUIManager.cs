using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerUIManager : Singleton<PlayerUIManager>
{ 
    public bool readyToDuel;

    public GameObject PlayerPiece;
    public GameObject PlayerPieceLocation;
    
    public GameObject PlayerHandCardZone;
    public GameObject PlayerDeck;
    public GameObject PlayerTrashCardZone;
    public GameObject PlayerState;
    public GameObject ReadyText;
    public PlayerDataManager PlayerData;
    public NewPlayerSkillManager PlayerSkill;

    public int[] Location;
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
        PlayerData.MoveToLocation[1] = 2;
        PlayerData.SettingValue();
        PlayerData.isPlayer1 = true;
        //PracticeLimited = false;
        PlayerData.NormalDrawAmount = PlayerData.StartHP;
        PlayerPiece.SetActive(false);
        PlayerHandCardZone.SetActive(false);
        PlayerDeck.SetActive(false);
        PlayerTrashCardZone.SetActive(false);
        PlayerState.SetActive(false);
        ReadyText.SetActive(false);
        StartCoroutine(StartDuel());
    }
    // Update is called once per frame
    void Update()
    {
        Location = PlayerData.PlayerLocation;
        PlayerData.EnemyLocation = EnemyUIManager.GetInstance().Location;
        //PlayerData.HP = PlayerHandCardZone
        if (PlayerData.isReady && (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move || DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack || DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Damage))
        {
            PlayerData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerReady;
            ReadyText.SetActive(true);
        }
        else
        {
            ReadyText.SetActive(false);
        }
        //PlayerSkill.GetComponent<SkillUI>().MaxHp.text = ":" + ThisPlayer.GetComponent<Player>().Hp.ToString() + "/" + ThisPlayer.GetComponent<Player>().MaxHp.ToString();
        //PlayerSkill.GetComponent<SkillUI>().Defense.text = ":" + ThisPlayer.GetComponent<Player>().Defense.ToString();
    }
    public void MovePiece()
    {
        PlayerPiece.transform.DOMove(PlayerPieceLocation.transform.GetChild(PlayerData.MoveToLocation[0]).transform.GetChild(PlayerData.MoveToLocation[1]).transform.position, 2);
        PlayerData.PlayerLocation = PlayerData.MoveToLocation;
    }
    public void PlayerReady()
    {
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move)
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
        else if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack)
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
            Debug.Log("Draw:" + PlayerData.NormalDrawAmount);
        }
        else if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Damage)
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
            Debug.Log("Draw:" + PlayerData.NormalDrawAmount);
        }
        PlayerData.canMove = true;
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
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Draw)
        {
            PlayerData.NormalDrawAmount += 1;
        }
        StartCoroutine(NormalDraw());
    }
    public void HealthDrawCard()
    {
        StartCoroutine(HealthDraw());
    }
    public virtual IEnumerator NormalDraw()
    {
        for (int i = 0; i < PlayerData.NormalDrawAmount; i++)
        {
            /*if (PlayerData.HP == PlayerData.MaxHP)
            {
                PlayerData.NormalDrawAmount = 0;
                yield return new WaitForSeconds(0.5f);
                PlayerData.isReady = true;
                yield break;
            }*/
            /*if (PlayerDeck.GetComponent<Deck>().isDeckNull)
            {
                yield return StartCoroutine(TrashCardBackDeck());
                yield return new WaitForSeconds(1);
            }*/
            /*if (PlayerDeck.GetComponent<Deck>().DeckAllCard.Count == 1)
            {
                PlayerDeck.GetComponent<Deck>().DeckImage.enabled = false;
            }*/
            var newcard = Instantiate(CardPrefab, PlayerDeck.transform);
            var decklist = PlayerDeck.GetComponent<PlayerDeck>().CardList;
            newcard.GetComponent<NewCardValueManager>().card = decklist[0];
            newcard.GetComponent<NewCardValueManager>().isPlayer = true;
            decklist.RemoveAt(0);
            while (newcard.transform.position != PlayerHandCardZone.transform.position)
            {
                newcard.transform.position = Vector2.MoveTowards(newcard.transform.position, PlayerHandCardZone.transform.position, 3000 * Time.deltaTime);
                yield return 0;
            }
            newcard.transform.parent = PlayerHandCardZone.transform; //卡片變成手牌子物件
            PlayerHandCardZone.transform.GetChild(index: PlayerHandCardZone.transform.childCount - 1).GetComponent<NewCardTurnTopOrBottom>().CardStartTop();
            yield return 0;
            /*var newcard = Instantiate(CardPrefab, PlayerDeck.transform);
            var decklist = PlayerDeck.GetComponent<PlayerDeck>().CardList;
            newcard.GetComponent<NewCardValueManager>().card = decklist[0];
            newcard.GetComponent<NewCardValueManager>().isPlayer = true;
            var thisdeckcard = PlayerDeck.transform.GetChild(0).gameObject;
            while (thisdeckcard.transform.position != PlayerHandCardZone.transform.position)
            {
                thisdeckcard.transform.position = Vector2.MoveTowards(thisdeckcard.transform.position, PlayerHandCardZone.transform.position, 3000 * Time.deltaTime);
                yield return 0;
            }
            Instantiate(newcard,PlayerHandCardZone.transform); //卡片變成手牌子物件
            decklist.RemoveAt(0);
            Destroy(thisdeckcard);
            PlayerHandCardZone.transform.GetChild(index: PlayerHandCardZone.transform.childCount - 1).GetComponent<NewCardTurnTopOrBottom>().CardStartTop();
            yield return 0;*/
        }
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Draw)
        {
            yield return new WaitForSeconds(1f);
            PlayerData.isReady = true;
        }
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.End)
        {
            if (PlayerData.NormalDrawAmount == 0)
            {
                yield return new WaitForSeconds(0.5f);
                PlayerData.isReady = true;
            }
            else
            {
                yield return new WaitForSeconds(1f);
                PlayerData.isReady = true;
            }
        }
        PlayerData.NormalDrawAmount = 0;
        yield return 0;
    }
    public virtual IEnumerator HealthDraw()
    {
        for (int i = 0; i < PlayerData.HealthValue; i++)
        {
            /*if (PlayerData.HP == PlayerData.MaxHP)
            {
                PlayerData.NormalDrawAmount = 0;
                yield return new WaitForSeconds(0.5f);
                PlayerData.isReady = true;
                yield break;
            }*/
            /*if (PlayerDeck.GetComponent<Deck>().isDeckNull)
            {
                yield return StartCoroutine(TrashCardBackDeck());
                yield return new WaitForSeconds(1);
            }*/
            /*if (PlayerDeck.GetComponent<Deck>().DeckAllCard.Count == 1)
            {
                PlayerDeck.GetComponent<Deck>().DeckImage.enabled = false;
            }*/
            var newcard = Instantiate(CardPrefab, PlayerDeck.transform);
            var decklist = PlayerDeck.GetComponent<PlayerDeck>().CardList;
            newcard.GetComponent<NewCardValueManager>().card = decklist[0];
            newcard.GetComponent<NewCardValueManager>().isPlayer = true;
            decklist.RemoveAt(0);
            while (newcard.transform.position != PlayerHandCardZone.transform.position)
            {
                newcard.transform.position = Vector2.MoveTowards(newcard.transform.position, PlayerHandCardZone.transform.position, 3000 * Time.deltaTime);
                yield return 0;
            }
            newcard.transform.parent = PlayerHandCardZone.transform; //卡片變成手牌子物件
            PlayerHandCardZone.transform.GetChild(index: PlayerHandCardZone.transform.childCount - 1).GetComponent<NewCardTurnTopOrBottom>().CardStartTop();
            yield return 0;
        }
        PlayerData.HealthValue = 0;
        yield return 0;
    }
    public virtual IEnumerator StartDuel()
    {
        yield return new WaitForSeconds(1);
        PlayerDeck.SetActive(true);
        PlayerDeck.GetComponent<PlayerDeck>().DeckStartSetting();
        PlayerTrashCardZone.SetActive(true);
        PlayerState.SetActive(true);
        PlayerHandCardZone.SetActive(true);
        yield return new WaitForSeconds(1);
        PlayerPiece.transform.position = PlayerPieceLocation.transform.GetChild(PlayerData.MoveToLocation[0]).transform.GetChild(PlayerData.MoveToLocation[1]).transform.position;
        PlayerData.PlayerLocation = PlayerData.MoveToLocation;
        PlayerPiece.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        //PlayerData.NormalDrawAmount = PlayerData.StartHP;
        yield return StartCoroutine(NormalDraw());
        readyToDuel = true;
        yield return 0;
    }
}
