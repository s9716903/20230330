using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewMoveResult : MonoBehaviour
{
    public GameObject PlayerResult;
    public GameObject EnemyResult;
    public GameObject Coin;

    public TextMeshProUGUI PlayerMoveValueText;
    public TextMeshProUGUI PlayerStarValueText;
    public TextMeshProUGUI PlayerDrawValueText;
    public TextMeshProUGUI PlayerFirstOrSecondText;
    public TextMeshProUGUI EnemyMoveValueText;
    public TextMeshProUGUI EnemyStarValueText;
    public TextMeshProUGUI EnemyDrawValueText;
    public TextMeshProUGUI EnemyFirstOrSecondText;

    private int CoinValue;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Coin.SetActive(false);
        if (!PracticeLimtedSetting.LimitedOn)
        {
            CoinValue = Random.Range(0, 2);
        }
        else
        {
            CoinValue = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var player = PlayerUIManager.GetInstance().PlayerData;
        var enemy = EnemyUIManager.GetInstance().EnemyData;
        PlayerMoveValueText.text = ":" + player.MoveValue.ToString();
        PlayerStarValueText.text = ":" + player.Stars.ToString();
        PlayerDrawValueText.text = ":" + player.HealthValue.ToString();
        EnemyMoveValueText.text = ":" + enemy.MoveValue.ToString();
        EnemyStarValueText.text = ":" + enemy.Stars.ToString();
        EnemyDrawValueText.text = ":" + enemy.HealthValue.ToString();
    }
    public IEnumerator StartResult()
    {
        //PracticeDialodue.CardLimitedOnShow = false;
        var player = PlayerUIManager.GetInstance().PlayerData;
        var enemy = EnemyUIManager.GetInstance().EnemyData;
        player.AllMoveStatePoint = player.MoveValue + player.Stars;
        enemy.AllMoveStatePoint = enemy.MoveValue + enemy.Stars;
        PlayerUIManager.GetInstance().MovePiece();
        EnemyUIManager.GetInstance().MovePiece();
        yield return new WaitForSeconds(2f);
        if (player.AllMoveStatePoint == enemy.AllMoveStatePoint)
        {
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
            yield return new WaitForSeconds(1f);
            Coin.SetActive(false);
        }
        if ((player.AllMoveStatePoint > enemy.AllMoveStatePoint) || ((player.AllMoveStatePoint > enemy.AllMoveStatePoint) && (CoinValue == 0)))
        {
            CameraManager.CameraInt = 1;
            CameraManager.ChangeCamera = true;
            PlayerFirstOrSecondText.text = "First Attack";
            PlayerResult.SetActive(true);
            EnemyResult.SetActive(false);
            yield return new WaitForSeconds(3f);
            CameraManager.CameraInt = 2;
            CameraManager.ChangeCamera = true;
            EnemyFirstOrSecondText.text = "Second Attack";
            PlayerResult.SetActive(false);
            EnemyResult.SetActive(true);
        }
        else if ((player.AllMoveStatePoint < enemy.AllMoveStatePoint) || ((player.AllMoveStatePoint == enemy.AllMoveStatePoint) && (CoinValue == 1)))
        {
            CameraManager.CameraInt = 2;
            CameraManager.ChangeCamera = true;
            EnemyFirstOrSecondText.text = "First Attack";
            PlayerResult.SetActive(false);
            EnemyResult.SetActive(true);
            yield return new WaitForSeconds(3f);
            CameraManager.CameraInt = 1;
            CameraManager.ChangeCamera = true;
            PlayerFirstOrSecondText.text = "Second Attack";
            PlayerResult.SetActive(true);
            EnemyResult.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        //PlayerTotal.SetActive(true);
        //Player2Total.SetActive(true);
        yield return new WaitForSeconds(1f);
        //Player1Draw.SetActive(true);
        //Player2Draw.SetActive(true);
        yield return new WaitForSeconds(2f);
        if (player.AllMoveStatePoint < enemy.AllMoveStatePoint)
        {
            //SecondAttackText.SetActive(true);
            player.isFirstATK = false;
            enemy.isFirstATK = true;
        }
        else if (player.AllMoveStatePoint > enemy.AllMoveStatePoint)
        {
            //FirstAttackText.SetActive(true);
            player.isFirstATK = true;
            enemy.isFirstATK = false;
        }
        else if (player.AllMoveStatePoint == enemy.AllMoveStatePoint)
        {
            var CoinAnimation = Coin.GetComponent<Animation>();
            //CoinAnimation.clip = CoinTopOrBottom[CoinValue];
            if (CoinValue == 0)
            {
                //CoinAni.SetActive(true);
                yield return new WaitForSeconds(3);
                //CoinAni.SetActive(false);
                //FirstAttackText.SetActive(true);
                player.isFirstATK = true;
                enemy.isFirstATK = false;
                Debug.Log("CoinValue:" + 0);
            }
            else if (CoinValue == 1)
            {
                //CoinAni.SetActive(true);
                yield return new WaitForSeconds(3);
                //CoinAni.SetActive(false);
                //SecondAttackText.SetActive(true);
                player.isFirstATK = false;
                enemy.isFirstATK = true;
                Debug.Log("CoinValue:" + 1);
            }
        }
        yield return new WaitForSeconds(2f);
    }
}
