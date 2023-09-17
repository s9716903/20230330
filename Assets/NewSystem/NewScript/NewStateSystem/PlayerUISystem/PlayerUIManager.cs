using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject CardPrefab;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        PlayerData = new PlayerDataManager();
        PlayerData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        PlayerData.NormalDrawAmount = 5;
        //PracticeLimited = false;
        PlayerData.isPlayer1 = true;
        PlayerPiece.SetActive(false);
        PlayerHandCardZone.SetActive(false);
        PlayerDeck.SetActive(false);
        PlayerTrashCardZone.SetActive(false);
        PlayerState.SetActive(false);
        ReadyText.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
           PlayerPieceLocation.transform.GetChild(i).GetComponent<NewLocation>().LocationGraph = i;
           PlayerPieceLocation.transform.GetChild(i).GetComponent<NewLocation>().playerData = PlayerData;
        }
        StartCoroutine(StartDuel());
    }
    // Update is called once per frame
    void Update()
    {
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
    public void MovePiece()
    {
        PlayerPiece.transform.position = PlayerPieceLocation.transform.GetChild(PlayerData.MoveToLocation).transform.position;
        PlayerData.PlayerLocation = PlayerData.MoveToLocation;
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
            yield return 0;
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
            newcard.GetComponent<NewCardValueManager>().playerData = PlayerData;
            var thisdeckcard = PlayerDeck.transform.GetChild(0).gameObject;
            while (thisdeckcard.transform.position != PlayerHandCardZone.transform.position)
            {
                thisdeckcard.transform.position = Vector2.MoveTowards(thisdeckcard.transform.position, PlayerHandCardZone.transform.position, 3000 * Time.deltaTime);
                yield return 0;
            }
            Instantiate(newcard, PlayerHandCardZone.transform); //卡片變成手牌子物件
            decklist.RemoveAt(0);
            Destroy(thisdeckcard);
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
        PlayerTrashCardZone.SetActive(true);
        PlayerState.SetActive(true);
        PlayerHandCardZone.SetActive(true);
        yield return new WaitForSeconds(1);
        MovePiece();
        PlayerPiece.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        //PlayerData.NormalDrawAmount = PlayerData.StartHP;
        yield return StartCoroutine(NormalDraw());
        readyToDuel = true;
        yield return 0;
    }
}
