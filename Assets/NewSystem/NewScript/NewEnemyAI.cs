using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewEnemyAI : MonoBehaviour
{
    public int HowManyCardUsing;
    // Start is called before the first frame update
    void Start()
    {
        HowManyCardUsing = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move) && (EnemyUIManager.GetInstance().EnemyData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerActivate))
        {
            if (NewTimer.StartTime == 50 || PlayerUIManager.GetInstance().PlayerData.isReady == true)
            {
                StartCoroutine(MoveState());
            }
        }
        else if ((DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack) && (EnemyUIManager.GetInstance().EnemyData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerActivate))
        {
            if (NewTimer.StartTime == 50 || PlayerUIManager.GetInstance().PlayerData.isReady)
            {
                StartCoroutine(AttackState());
            }
        }
        else if ((DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Damage) && (EnemyUIManager.GetInstance().EnemyData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerActivate))
        {
            if (NewTimer.StartTime == 20 || PlayerUIManager.GetInstance().PlayerData.isReady)
            {
                StartCoroutine(DamageState());
            }
        }
    }
    public IEnumerator CanUseThisCard()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if ((transform.GetChild(i).GetComponent<NewCardValueManager>().cardcanuse == false) || (transform.GetChild(i).GetComponent<NewCardValueManager>().MainType == 3))
            {
                transform.GetChild(i).GetComponent<NewCardValueManager>().isCardUp = !transform.GetChild(i).GetComponent<NewCardValueManager>().isCardUp;
            }
        }
        yield return 0;
    }
    public IEnumerator MoveState()
    {
        yield return StartCoroutine(CanUseThisCard());
        if (PlayerUIManager.GetInstance().PlayerData.isReady == true)
        {
            yield return new WaitForSeconds(1);
        }
        //var handcard = GetComponent<PlayerHandCardManager>();
        var howmanyuse = 1;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<NewCardValueManager>().MainType == 0 && i < howmanyuse)
            {
                transform.GetChild(i).gameObject.GetComponent<NewCardValueManager>().ChooseUseCard();
            }
            else if (transform.GetChild(i).GetComponent<NewCardValueManager>().MainType == 3 && i < howmanyuse)
            {
                transform.GetChild(i).gameObject.GetComponent<NewCardValueManager>().ChooseUseCard();
            }
            else if (transform.GetChild(i).GetComponent<NewCardValueManager>().MainType == 4 && i < howmanyuse)
            {
                transform.GetChild(i).gameObject.GetComponent<NewCardValueManager>().ChooseUseCard();
            }
        }
        //handcard.PlayerCardValueReady();
        if (!PracticeLimtedSetting.LimitedOn)
        {
            //EnemyUIManager.GetInstance().EnemyData.MoveToLocation = Random.Range(EnemyUIManager.GetInstance().EnemyData.MoveToLocation - EnemyUIManager.GetInstance().EnemyData.MoveValue, EnemyUIManager.GetInstance().EnemyData.MoveToLocation + EnemyUIManager.GetInstance().EnemyData.MoveValue + 1);
        }
        else
        {
            //EnemyUIManager.GetInstance().EnemyData.MoveToLocation = PracticeLimtedSetting.EnemyLocation;
        }
        for (int j = 0; j < transform.childCount; j++)
        {
            var targetcard = transform.GetChild(j);
            if (targetcard.GetComponent<NewCardValueManager>().usecard == false)
            {
                targetcard.gameObject.SetActive(false);
            }
        }
        EnemyUIManager.GetInstance().EnemyReady();
        EnemyUIManager.GetInstance().EnemyData.isReady = true;
        yield return 0;
    }
    public IEnumerator AttackState()
    {
        yield return StartCoroutine(CanUseThisCard());
        if (PlayerUIManager.GetInstance().PlayerData.isReady == true)
        {
            yield return new WaitForSeconds(1);
        }
        //var handcard = GetComponent<PlayerHandCardManager>();
        var howmanyuse = 1;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<NewCardValueManager>().MainType == 1 && i < howmanyuse)
            {
                transform.GetChild(i).gameObject.GetComponent<NewCardValueManager>().ChooseUseCard();
            }
            else if (transform.GetChild(i).GetComponent<NewCardValueManager>().MainType == 2 && i < howmanyuse)
            {
                transform.GetChild(i).gameObject.GetComponent<NewCardValueManager>().ChooseUseCard();
            }
            else if (transform.GetChild(i).GetComponent<NewCardValueManager>().MainType == 3 && i < howmanyuse)
            {
                transform.GetChild(i).gameObject.GetComponent<NewCardValueManager>().ChooseUseCard();
            }
            else if (transform.GetChild(i).GetComponent<NewCardValueManager>().MainType == 4 && i < howmanyuse)
            {
                transform.GetChild(i).gameObject.GetComponent<NewCardValueManager>().ChooseUseCard();
            }
        }
        //handcard.PlayerCardValueReady();
        for (int j = 0; j < transform.childCount; j++)
        {
            var targetcard = transform.GetChild(j);
            if (targetcard.GetComponent<NewCardValueManager>().usecard == false)
            {
                targetcard.gameObject.SetActive(false);
            }
        }
        yield return 0;
        EnemyUIManager.GetInstance().EnemyReady();
        EnemyUIManager.GetInstance().EnemyData.isReady = true;
        yield return 0;
    }
    public IEnumerator DamageState()
    {
        HowManyCardUsing = 0;
        if (PlayerUIManager.GetInstance().PlayerData.isReady == true)
        {
            yield return new WaitForSeconds(1);
        }
        //var handcard = GetComponent<PlayerHandCardManager>();
        var howmanyuse = EnemyUIManager.GetInstance().EnemyData.AllDamaged;
        if (PracticeLimtedSetting.LimitedOn)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.GetComponent<NewCardValueManager>().ChooseDamageDropCard();
            }
            for (int j = 0; j < transform.childCount; j++)
            {
                var targetcard = transform.GetChild(j);
                if (targetcard.GetComponent<NewCardValueManager>().usecard == false)
                {
                    targetcard.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            /*for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<CardManager>().ID != 3 && HowManyCardUsing < howmanyuse)
                {
                    transform.GetChild(i).gameObject.GetComponent<CardManager>().ChooseDamageDropCard();
                    HowManyCardUsing++;
                }
            }
            for (int j = 0; j < transform.childCount; j++)
            {
                transform.GetChild(j).GetComponent<CardManager>().isCardUp = !transform.GetChild(j).GetComponent<CardManager>().isCardUp;
                if (transform.GetChild(j).GetComponent<CardManager>().ID != 3 && HowManyCardUsing < howmanyuse)
                {
                    transform.GetChild(j).gameObject.GetComponent<CardManager>().ChooseDamageDropCard();
                    HowManyCardUsing++;
                }
            }
            for (int k = 0; k < transform.childCount; k++)
            {
                if (transform.GetChild(k).GetComponent<CardManager>().ID == 3 && HowManyCardUsing < howmanyuse)
                {
                    transform.GetChild(k).gameObject.GetComponent<CardManager>().ChooseDamageDropCard();
                    HowManyCardUsing++;
                }
            }
            for (int l = 0; l < transform.childCount; l++)
            {
                var targetcard = transform.GetChild(l);
                if (targetcard.GetComponent<CardManager>().DamagedDropCard == false)
                {
                    targetcard.gameObject.SetActive(false);
                }
            }*/
        }
        EnemyUIManager.GetInstance().EnemyData.isReady = true;
        yield return 0;
    }
}
