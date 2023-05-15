using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackResult : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Player1Value;
    public GameObject Player2Value;
    public GameObject PlayerTotalDamaged;
    public GameObject Player2TotalDamaged;

    public TextMeshProUGUI Player1PhysicATKValueText;
    public TextMeshProUGUI Player1MagicATKValueText;
    public TextMeshProUGUI Player1DrawValueText;
    public TextMeshProUGUI Player2PhysicATKValueText;
    public TextMeshProUGUI Player2MagicATKValueText;
    public TextMeshProUGUI Player2DrawValueText;
    public TextMeshProUGUI PlayerTotalDamagedValueText;
    public TextMeshProUGUI Player2TotalDamagedValueText;
    public GameObject Player1Life;
    public GameObject Player2Life;

    private AudioSource audiosource;
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    private void OnEnable()
    {
        audiosource = GetComponent<AudioSource>();
        Player1Value.SetActive(false);
        Player2Value.SetActive(false);
        PlayerTotalDamaged.SetActive(false);
        Player2TotalDamaged.SetActive(false);
        Player1Life.SetActive(false);
        Player2Life.SetActive(false);
        StartCoroutine(StartResult());
    }
    // Update is called once per frame
    void Update()
    {
        var player = Player.GetComponent<Player>();
        var enemy = Enemy.GetComponent<Player>();
        Player1PhysicATKValueText.text = ":" + enemy.PhysicDamage.ToString();
        Player1MagicATKValueText.text = ":" + enemy.MagicDamage.ToString();
        Player1DrawValueText.text = ":" + player.HealthDrawAmount.ToString();
        PlayerTotalDamagedValueText.text = "-" + player.AllDamaged.ToString();
        Player2PhysicATKValueText.text = player.PhysicDamage.ToString() + ":";
        Player2MagicATKValueText.text = player.MagicDamage.ToString() + ":";
        Player2DrawValueText.text = enemy.HealthDrawAmount.ToString() + ":";
        Player2TotalDamagedValueText.text = "-" + enemy.AllDamaged.ToString();
    }
    public IEnumerator StartResult()
    {
        var player = Player.GetComponent<Player>();
        var enemy = Enemy.GetComponent<Player>();
        var player_hp = player.AttackResultHP + player.HealthDrawAmount;
        var enemy_hp = enemy.AttackResultHP + enemy.HealthDrawAmount;
        if (player_hp > player.MaxHp)
        {
            player_hp = player.MaxHp;
        }
        if (enemy_hp > enemy.MaxHp)
        {
            enemy_hp = enemy.MaxHp;
        }
        yield return new WaitForSeconds(1f);
        if (player.isFirstATK)
        {
            Player1Value.SetActive(true);
            Player2Value.SetActive(true);
            yield return new WaitForSeconds(2);
            Player2TotalDamaged.SetActive(true);
            if (player.TargetLocation == enemy.TargetLocation)
            {
                audiosource.clip = audioClips[0];
                audiosource.Play();
            }
            else
            {
                audiosource.clip = audioClips[1];
                audiosource.Play();
            }
            yield return new WaitForSeconds(2f);
            if (enemy_hp <= enemy.AllDamaged)
            {
                Player2Life.GetComponent<TextMeshProUGUI>().text = "Die";
                Player2Life.SetActive(true);
                yield return new WaitForSeconds(2);
                DuelUIController.player2lose = true;
                yield break;

            }
            else if (enemy_hp > enemy.AllDamaged)
            {
                Player2Life.GetComponent<TextMeshProUGUI>().text = "Alive";
                Player2Life.SetActive(true);
            }
            yield return new WaitForSeconds(2);
            PlayerTotalDamaged.SetActive(true);
            if (player.TargetLocation == enemy.TargetLocation)
            {
                audiosource.clip = audioClips[0];
                audiosource.Play();
            }
            else
            {
                audiosource.clip = audioClips[1];
                audiosource.Play();
            }
            yield return new WaitForSeconds(2f);
            if (player_hp <= player.AllDamaged)
            {
                Player1Life.GetComponent<TextMeshProUGUI>().text = "Die";
                Player1Life.SetActive(true);
                yield return new WaitForSeconds(2);
                DuelUIController.player1lose = true;
                yield break;
            }
            else if (player_hp > player.AllDamaged)
            {
                Player1Life.GetComponent<TextMeshProUGUI>().text = "Alive";
                Player1Life.SetActive(true);
            }
        }
        else if (enemy.isFirstATK)
        {
            Player1Value.SetActive(true);
            Player2Value.SetActive(true);
            yield return new WaitForSeconds(2f);
            PlayerTotalDamaged.SetActive(true);
            if (player.TargetLocation == enemy.TargetLocation)
            {
                audiosource.clip = audioClips[0];
                audiosource.Play();
            }
            else
            {
                audiosource.clip = audioClips[1];
                audiosource.Play();
            }
            yield return new WaitForSeconds(2f);
            if (player_hp <= player.AllDamaged)
            {
                Player1Life.GetComponent<TextMeshProUGUI>().text = "Die";
                Player1Life.SetActive(true);
                yield return new WaitForSeconds(2);
                if (PracticeLimtedSetting.LimitedOn)
                {
                    DuelUIController.PracticeEnd = true;
                    yield break;
                }
                else
                { 
                    DuelUIController.player1lose = true;
                    yield break;
                }
            }
            else if (player_hp > player.AllDamaged)
            {
                Player1Life.GetComponent<TextMeshProUGUI>().text = "Alive";
                Player1Life.SetActive(true);
            }
            yield return new WaitForSeconds(2);
            Player2TotalDamaged.SetActive(true);
            if (player.TargetLocation == enemy.TargetLocation)
            {
                audiosource.clip = audioClips[0];
                audiosource.Play();
            }
            else
            {
                audiosource.clip = audioClips[1];
                audiosource.Play();
            }
            yield return new WaitForSeconds(2f);
            if (enemy_hp <= enemy.AllDamaged)
            {
                Player2Life.GetComponent<TextMeshProUGUI>().text = "Die";
                Player2Life.SetActive(true);
                yield return new WaitForSeconds(2);
                DuelUIController.player2lose = true;
                yield break;
            }
            else if (enemy_hp > enemy.AllDamaged)
            {
                Player2Life.GetComponent<TextMeshProUGUI>().text = "Alive";
                Player2Life.SetActive(true);
            }
        }
        yield return new WaitForSeconds(2f);
    }
}
