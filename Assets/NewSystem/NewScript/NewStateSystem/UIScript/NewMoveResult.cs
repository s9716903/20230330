using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMoveResult : MonoBehaviour
{
    public GameObject Coin;
    private int CoinValue;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Coin.SetActive(false);
        CoinValue = Random.Range(0, 2);
        /*if (!PracticeLimtedSetting.LimitedOn)
        {
            CoinValue = Random.Range(0, 2);
        }
        else
        {
            CoinValue = 1;
        }*/
    }
    // Update is called once per frame
    public IEnumerator MoveResult()
    {
        DuelUIManager.showInformationText = true;
        DuelUIManager.Information = "Move Result";
        //PracticeDialodue.CardLimitedOnShow = false;
        PlayerUIManager.GetInstance().MovePiece();
        //EnemyManager.GetInstance().MovePiece();
        yield return new WaitForSeconds(1f);
        DuelUIManager.Information = "First or second";
        Coin.SetActive(true);
        if (CoinValue == 0)
        {
            CoinController.isCoinTop = true;
            yield return StartCoroutine(Coin.GetComponent<CoinController>().CoinTopOrBottom());
        }
        else
        {
            CoinController.isCoinTop = false;
            yield return StartCoroutine(Coin.GetComponent<CoinController>().CoinTopOrBottom());
        }
        yield return new WaitForSeconds(0.5f);
        if (CoinValue == 0)
        {
            PlayerUIManager.GetInstance().isFirstATK = true;
            DuelUIManager.Information = "You Are First";
        }
        else
        {
            PlayerUIManager.GetInstance().isFirstATK = false;
            DuelUIManager.Information = "You Are Second";
        }
        yield return new WaitForSeconds(1f);
        Coin.SetActive(false);
        DuelUIManager.showInformationText = false;
        yield return null;
    }
}
