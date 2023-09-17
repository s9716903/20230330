using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUIManager : Singleton<EnemyUIManager>
{
    public bool readyToDuel;

    public GameObject EnemyPiece;
    public GameObject EnemyPieceLocation;

    public GameObject EnemyHandCardZone;
    public GameObject EnemyDeck;
    public GameObject EnemyTrashCardZone;
    public GameObject EnemyState;
    public GameObject ReadyText;
    public PlayerDataManager EnemyData;

    public GameObject CardPrefab;
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        EnemyData = new PlayerDataManager();
        EnemyData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        EnemyData.NormalDrawAmount = 5;
        //PracticeLimited = false;
        EnemyData.isPlayer1 = false;
        EnemyPiece.SetActive(false);
        EnemyHandCardZone.SetActive(false);
        EnemyDeck.SetActive(false);
        EnemyTrashCardZone.SetActive(false);
        EnemyState.SetActive(false);
        ReadyText.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            EnemyPieceLocation.transform.GetChild(i).GetComponent<NewLocation>().LocationGraph = i;
            EnemyPieceLocation.transform.GetChild(i).GetComponent<NewLocation>().playerData = EnemyData;
        }
        StartCoroutine(StartDuel());
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyData.isReady && (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move || DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack || DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.DamageResult))
        {
            EnemyData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerReady;
            ReadyText.SetActive(true);
        }
        else
        {
            ReadyText.SetActive(false);
        }
        //PlayerSkill.GetComponent<SkillUI>().MaxHp.text = ":" + ThisPlayer.GetComponent<Player>().Hp.ToString() + "/" + ThisPlayer.GetComponent<Player>().MaxHp.ToString();
        //PlayerSkill.GetComponent<SkillUI>().Defense.text = ":" + ThisPlayer.GetComponent<Player>().Defense.ToString();
    }
    public void EnemyReady()
    {
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move)
        {
            for (int i = 0; i < EnemyHandCardZone.transform.childCount; i++)
            {
                var targetcard = EnemyHandCardZone.transform.GetChild(i);
                if (targetcard.GetComponent<NewCardValueManager>().usecard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 10, 0);
                }
            }
            for (int j = 0; j < EnemyHandCardZone.transform.childCount; j++)
            {
                var targetcard = EnemyHandCardZone.transform.GetChild(j);
                if (targetcard.GetComponent<NewCardValueManager>().usecard == false)
                {
                    targetcard.gameObject.SetActive(false);
                }
            }
        }
        else if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack)
        {
            for (int i = 0; i < EnemyHandCardZone.transform.childCount; i++)
            {
                var targetcard = EnemyHandCardZone.transform.GetChild(i);
                if (targetcard.GetComponent<NewCardValueManager>().usecard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 10, 0);
                }
            }
            for (int j = 0; j < EnemyHandCardZone.transform.childCount; j++)
            {
                var targetcard = EnemyHandCardZone.transform.GetChild(j);
                if (targetcard.GetComponent<NewCardValueManager>().usecard == false)
                {
                    targetcard.gameObject.SetActive(false);
                }
            }
            Debug.Log("EnemyDraw:" + EnemyData.NormalDrawAmount);
        }
        else if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Damage)
        {
            for (int i = 0; i < EnemyHandCardZone.transform.childCount; i++)
            {
                var targetcard = EnemyHandCardZone.transform.GetChild(i);
                if (targetcard.GetComponent<NewCardValueManager>().usecard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 10, 0);
                }
            }
            for (int j = 0; j < EnemyHandCardZone.transform.childCount; j++)
            {
                var targetcard = EnemyHandCardZone.transform.GetChild(j);
                if (targetcard.GetComponent<NewCardValueManager>().usecard == false)
                {
                    targetcard.gameObject.SetActive(false);
                }
            }
            Debug.Log("EnemyDraw:" + EnemyData.NormalDrawAmount);
        }
        EnemyData.canMove = true;
    }
    public void NormalDrawCard()
    {
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Draw)
        {
            EnemyData.NormalDrawAmount += 1;
        }
        StartCoroutine(NormalDraw());
    }
    public void HealthDrawCard()
    {
        StartCoroutine(HealthDraw());
    }
    public void MovePiece()
    {
        EnemyPiece.transform.position = EnemyPieceLocation.transform.GetChild(EnemyData.MoveToLocation).transform.position;
        EnemyData.PlayerLocation = EnemyData.MoveToLocation;
    }
    public virtual IEnumerator NormalDraw()
    {
        for (int i = 0; i < EnemyData.NormalDrawAmount; i++)
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
            var newcard = Instantiate(CardPrefab, EnemyDeck.transform);
            var decklist = EnemyDeck.GetComponent<PlayerDeck>().CardList;
            newcard.GetComponent<NewCardValueManager>().card = decklist[0];
            newcard.GetComponent<NewCardValueManager>().playerData = EnemyData;
            var thisdeckcard = EnemyDeck.transform.GetChild(0).gameObject;
            while (thisdeckcard.transform.position != EnemyHandCardZone.transform.position)
            {
                thisdeckcard.transform.position = Vector2.MoveTowards(thisdeckcard.transform.position, EnemyHandCardZone.transform.position, 3000 * Time.deltaTime);
                yield return 0;
            }
            Instantiate(newcard, EnemyHandCardZone.transform); //卡片變成手牌子物件
            decklist.RemoveAt(0);
            Destroy(thisdeckcard);
            yield return 0;
        }
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Draw)
        {
            yield return new WaitForSeconds(1f);
            EnemyData.isReady = true;
        }
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.End)
        {
            if (EnemyData.NormalDrawAmount == 0)
            {
                yield return new WaitForSeconds(0.5f);
                EnemyData.isReady = true;
            }
            else
            {
                yield return new WaitForSeconds(1f);
                EnemyData.isReady = true;
            }
        }
        EnemyData.NormalDrawAmount = 0;
        yield return 0;
    }
    public virtual IEnumerator HealthDraw()
    {
        for (int i = 0; i < EnemyData.HealthValue; i++)
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
            var newcard = Instantiate(CardPrefab, EnemyDeck.transform);
            var decklist = EnemyDeck.GetComponent<PlayerDeck>().CardList;
            newcard.GetComponent<NewCardValueManager>().card = decklist[0];
            newcard.GetComponent<NewCardValueManager>().playerData = EnemyData;
            var thisdeckcard = EnemyDeck.transform.GetChild(0).gameObject;
            while (thisdeckcard.transform.position != EnemyHandCardZone.transform.position)
            {
                thisdeckcard.transform.position = Vector2.MoveTowards(thisdeckcard.transform.position, EnemyHandCardZone.transform.position, 3000 * Time.deltaTime);
                yield return 0;
            }
            Instantiate(newcard, EnemyHandCardZone.transform); //卡片變成手牌子物件
            decklist.RemoveAt(0);
            Destroy(thisdeckcard);
            yield return 0;
        }
        EnemyData.HealthValue = 0;
        yield return 0;
    }
    public IEnumerator StartDuel()
    {
        yield return new WaitForSeconds(1);
        EnemyDeck.SetActive(true);
        EnemyTrashCardZone.SetActive(true);
        EnemyState.SetActive(true);
        EnemyHandCardZone.SetActive(true);
        yield return new WaitForSeconds(1);
        MovePiece();
        EnemyPiece.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        //PlayerData.NormalDrawAmount = PlayerData.StartHP;
        yield return StartCoroutine(NormalDraw());
        readyToDuel = true;
        yield return 0;
    }
}
