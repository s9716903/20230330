using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveResult : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Player1Value;
    public GameObject Player2Value;
    public GameObject PlayerTotal;
    public GameObject Player2Total;
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

    // Start is called before the first frame update
    private void OnEnable()
    {
        CoinAni.SetActive(false);
        Player1Value.SetActive(false);
        Player2Value.SetActive(false);
        PlayerTotal.SetActive(false);
        Player2Total.SetActive(false);
        FirstAttackText.SetActive(false);
        SecondAttackText.SetActive(false);
        StartCoroutine(StartResult());
    }

    // Update is called once per frame
    void Update()
    {
        var player = Player.GetComponent<Player>();
        var enemy = Enemy.GetComponent<Player>();
        Player1MoveValueText.text = player.MoveValue.ToString();
        Player1StarValueText.text = player.Stars.ToString();
        Player1DrawValueText.text = player.HealthDrawAmount.ToString();
        PlayerMovePointTotalText.text = player.MoveStatePoint.ToString();
        Player2MoveValueText.text = enemy.MoveValue.ToString();
        Player2StarValueText.text = enemy.Stars.ToString();
        Player2DrawValueText.text = enemy.HealthDrawAmount.ToString();
        Player2MovePointTotalText.text = enemy.MoveStatePoint.ToString();
    }
    public IEnumerator StartResult()
    {
        var player = Player.GetComponent<Player>();
        var enemy = Enemy.GetComponent<Player>();
        yield return new WaitForSeconds(1f);
        Player1Value.SetActive(true);
        Player2Value.SetActive(true);
        yield return new WaitForSeconds(1f);
        PlayerTotal.SetActive(true);
        Player2Total.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (player.MoveStatePoint < enemy.MoveStatePoint)
        {
            SecondAttackText.SetActive(true);
        }
        else if (player.MoveStatePoint > enemy.MoveStatePoint)
        {
            FirstAttackText.SetActive(true);
        }
        else if (player.MoveStatePoint == enemy.MoveStatePoint)
        {
            var CoinValue = Random.Range(0, 2);
            var CoinAnimation = Coin.GetComponent<Animation>();
            CoinAnimation.clip = CoinTopOrBottom[CoinValue];
            if (CoinValue == 0)
            {
                CoinAni.SetActive(true);
                yield return new WaitForSeconds(3);
                CoinAni.SetActive(false);
                FirstAttackText.SetActive(true);
                Debug.Log("CoinValue:" + 0);
            }
            else if(CoinValue == 1)
            {
                CoinAni.SetActive(true);
                yield return new WaitForSeconds(3);
                CoinAni.SetActive(false);
                SecondAttackText.SetActive(true);
                Debug.Log("CoinValue:" + 1);
            }
        }
        yield return new WaitForSeconds(2f);
    }
}
