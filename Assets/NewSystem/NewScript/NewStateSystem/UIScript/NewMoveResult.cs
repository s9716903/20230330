using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewMoveResult : MonoBehaviour
{
    public GameObject Player1Value;
    public GameObject Player2Value;
    public GameObject PlayerTotal;
    public GameObject Player2Total;
    public GameObject Player1Draw;
    public GameObject Player2Draw;
    public GameObject CoinAni;
    public GameObject Coin;
    public AnimationClip[] CoinTopOrBottom;

    public TextMeshProUGUI Player1MoveValueText;
    public TextMeshProUGUI Player1StarValueText;
    public TextMeshProUGUI Player1DrawValueText;
    public TextMeshProUGUI Player2MoveValueText;
    public TextMeshProUGUI Player2StarValueText;
    public TextMeshProUGUI Player2DrawValueText;
    public TextMeshProUGUI PlayerMovePointTotalText;
    public TextMeshProUGUI Player2MovePointTotalText;
    public GameObject FirstAttackText;
    public GameObject SecondAttackText;

    private int CoinValue;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (!PracticeLimtedSetting.LimitedOn)
        {
            CoinValue = Random.Range(0, 2);
        }
        else
        {
            CoinValue = 1;
        }
        CoinAni.SetActive(false);
        Player1Value.SetActive(false);
        Player2Value.SetActive(false);
        PlayerTotal.SetActive(false);
        Player2Total.SetActive(false);
        Player1Draw.SetActive(false);
        Player2Draw.SetActive(false);
        FirstAttackText.SetActive(false);
        SecondAttackText.SetActive(false);
        StartCoroutine(StartResult());
    }

    // Update is called once per frame
    void Update()
    {
        var player = PlayerUIManager.GetInstance().PlayerData;
        var enemy = EnemyUIManager.GetInstance().EnemyData;
        Player1MoveValueText.text = ":" + player.MoveValue.ToString();
        Player1StarValueText.text = ":" + player.Stars.ToString();
        Player1DrawValueText.text = ":" + player.HealthValue.ToString();
        PlayerMovePointTotalText.text = (player.MoveValue + player.Stars).ToString();
        Player2MoveValueText.text = enemy.MoveValue.ToString() + ":";
        Player2StarValueText.text = enemy.Stars.ToString() + ":";
        Player2DrawValueText.text = enemy.HealthValue.ToString() + ":";
        Player2MovePointTotalText.text = (enemy.MoveValue + enemy.Stars).ToString();
    }
    public IEnumerator StartResult()
    {
        PracticeDialodue.CardLimitedOnShow = false;
        var player = PlayerUIManager.GetInstance().PlayerData;
        var enemy = EnemyUIManager.GetInstance().EnemyData;
        yield return new WaitForSeconds(1f);
        Player1Value.SetActive(true);
        Player2Value.SetActive(true);
        yield return new WaitForSeconds(1f);
        PlayerTotal.SetActive(true);
        Player2Total.SetActive(true);
        yield return new WaitForSeconds(1f);
        Player1Draw.SetActive(true);
        Player2Draw.SetActive(true);
        yield return new WaitForSeconds(2f);
        if (player.MoveValue + player.Stars < enemy.MoveValue + enemy.Stars)
        {
            SecondAttackText.SetActive(true);
            player.isFirstATK = false;
            enemy.isFirstATK = true;
        }
        else if (player.MoveValue + player.Stars > enemy.MoveValue + enemy.Stars)
        {
            FirstAttackText.SetActive(true);
            player.isFirstATK = true;
            enemy.isFirstATK = false;
        }
        else if (player.MoveValue + player.Stars == enemy.MoveValue + enemy.Stars)
        {
            var CoinAnimation = Coin.GetComponent<Animation>();
            CoinAnimation.clip = CoinTopOrBottom[CoinValue];
            if (CoinValue == 0)
            {
                CoinAni.SetActive(true);
                yield return new WaitForSeconds(3);
                CoinAni.SetActive(false);
                FirstAttackText.SetActive(true);
                player.isFirstATK = true;
                enemy.isFirstATK = false;
                Debug.Log("CoinValue:" + 0);
            }
            else if (CoinValue == 1)
            {
                CoinAni.SetActive(true);
                yield return new WaitForSeconds(3);
                CoinAni.SetActive(false);
                SecondAttackText.SetActive(true);
                player.isFirstATK = false;
                enemy.isFirstATK = true;
                Debug.Log("CoinValue:" + 1);
            }
        }
        yield return new WaitForSeconds(2f);
    }
}
