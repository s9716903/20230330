using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIController : MonoBehaviour
{
    [Header("ThinkingTimeSetting")]
    public int LeastThinkingTime;
    public int MaxThinkingTime;
    public int ThinkingTime;
  
    public int HowManyCardCanUse;
    public int HowManyCardUsing;

    public GameObject ThisPlayer;
    public GameObject ThisEnemy;

    public static bool AIDoThing;

    // Start is called before the first frame update
    void Start()
    {
        HowManyCardCanUse = 0;
        HowManyCardUsing = 0;
        AIDoThing = false;
        ThinkingTime = Random.Range(LeastThinkingTime, MaxThinkingTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.Move && DuelStateManager.playerStateType == GameState.PlayerStateMode.DoThing)
        {
            if ((StateTimer.startTime == ThinkingTime || ThisEnemy.GetComponent<Player>().isReady == true) && AIDoThing == false)
            {
                StartCoroutine(MoveState());
            }
        }
        else if (DuelStateManager.duelStateType == GameState.DuelStateMode.Attack && DuelStateManager.playerStateType == GameState.PlayerStateMode.DoThing)
        {
            if ((StateTimer.startTime == ThinkingTime || ThisEnemy.GetComponent<Player>().isReady) && AIDoThing == false)
            {
                StartCoroutine(AttackState());
            }
        }
        else if (DuelStateManager.duelStateType == GameState.DuelStateMode.AttackResult && DuelStateManager.playerStateType == GameState.PlayerStateMode.Damage)
        {
            if ((StateTimer.startTime == ThinkingTime || ThisEnemy.GetComponent<Player>().isReady) && AIDoThing == false)
            {
                StartCoroutine(DamageState());
            }
        }
    }
    public IEnumerator CanUseThisCard()
    {
        HowManyCardCanUse = 0;
        HowManyCardUsing = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<CardManager>().canUseThisCard == false)
            {
                transform.GetChild(i).GetComponent<CardManager>().isCardUp = !transform.GetChild(i).GetComponent<CardManager>().isCardUp;
            }
        }
        yield return 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<CardManager>().canUseThisCard == true)
            {
                HowManyCardCanUse++;
            }
        }
    }
    public IEnumerator MoveState()
    {
        AIDoThing = true;
        ThinkingTime = Random.Range(LeastThinkingTime, MaxThinkingTime);
        yield return StartCoroutine(CanUseThisCard());
        if (ThisEnemy.GetComponent<Player>().isReady == true)
        {
            yield return new WaitForSeconds(1);
        }
        var AIplayeer = ThisPlayer.GetComponent<Player>();
        var handcard = GetComponent<HandCards>();
        var howmanyuse = Random.Range(0, HowManyCardCanUse - 1);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<CardManager>().ID == 0 && HowManyCardUsing <= howmanyuse)
            {
                transform.GetChild(i).gameObject.GetComponent<CardManager>().ChooseUseCard();
                ThisPlayer.GetComponent<Player>().NormalDrawAmount++;
            }
            else if (transform.GetChild(i).GetComponent<CardManager>().ID == 3 && HowManyCardUsing <= howmanyuse)
            {
                transform.GetChild(i).gameObject.GetComponent<CardManager>().ChooseUseCard();
            }
        }
        handcard.PlayerCardValueReady();
        AIplayeer.MoveToLocation = Random.Range(AIplayeer.TargetLocation - AIplayeer.MoveValue, AIplayeer.TargetLocation + AIplayeer.MoveValue + 1);
        for (int j = 0; j < transform.childCount; j++)
        {
            var targetcard = transform.GetChild(j);
            if ((targetcard.GetComponent<CardManager>().isUseThisCard == false) && (targetcard.GetComponent<CardManager>().isDropThisCard == false))
            {
                targetcard.gameObject.SetActive(false);
            }
        }
        AIplayeer.canMove = true;
        AIplayeer.isReady = true;
        yield return 0;
    }
    public IEnumerator AttackState()
    {
        AIDoThing = true;
        ThinkingTime = Random.Range(LeastThinkingTime, MaxThinkingTime);
        yield return StartCoroutine(CanUseThisCard());
        if (ThisEnemy.GetComponent<Player>().isReady == true)
        {
            yield return new WaitForSeconds(1);
        }
        var AIplayeer = ThisPlayer.GetComponent<Player>();
        var handcard = GetComponent<HandCards>();
        var howmanyuse = Random.Range(0, HowManyCardCanUse - 1);
        if (!AIplayeer.isFirstATK)
        {
            howmanyuse = 0;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<CardManager>().ID == 1 && HowManyCardUsing <= howmanyuse)
            {
                if (transform.GetChild(i).GetComponent<CardManager>().Type == 0)
                {
                    transform.GetChild(i).gameObject.GetComponent<CardManager>().ChooseUseCard();
                    ThisPlayer.GetComponent<Player>().NormalDrawAmount++;
                    HowManyCardUsing++;
                }
                else if (transform.GetChild(i).GetComponent<CardManager>().Type == 1)
                {
                    transform.GetChild(i).gameObject.GetComponent<CardManager>().ChooseUseCard();
                    ThisPlayer.GetComponent<Player>().NormalDrawAmount++;
                    HowManyCardUsing++;
                }
            }
            else if (transform.GetChild(i).GetComponent<CardManager>().ID == 3 && HowManyCardUsing <= howmanyuse)
            {
                transform.GetChild(i).gameObject.GetComponent<CardManager>().ChooseUseCard();
                HowManyCardUsing++;
            }
        }
        handcard.PlayerCardValueReady();
        for (int j = 0; j < transform.childCount; j++)
        {
            var targetcard = transform.GetChild(j);
            if ((targetcard.GetComponent<CardManager>().isUseThisCard == false) && (targetcard.GetComponent<CardManager>().isDropThisCard == false))
            {
                targetcard.gameObject.SetActive(false);
            }
        }
        AIplayeer.canMove = true;
        AIplayeer.isReady = true;
        yield return 0;
    }
    public IEnumerator DamageState()
    {
        AIDoThing = true;
        HowManyCardUsing = 0;
        ThinkingTime = Random.Range(LeastThinkingTime,30);
        if (ThisEnemy.GetComponent<Player>().isReady == true)
        {
            yield return new WaitForSeconds(1);
        }
        var AIplayeer = ThisPlayer.GetComponent<Player>();
        var handcard = GetComponent<HandCards>();
        var howmanyuse = AIplayeer.AllDamaged;
        for (int i = 0; i < transform.childCount; i++)
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
        }
        AIplayeer.canMove = true;
        AIplayeer.isReady = true;
        yield return 0;
    }
}
