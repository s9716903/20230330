using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CharacterPiece : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //跳出效果UI
        if ((PlayerUIManager.GetInstance().PlayerData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerActivate))
        {
            StartCoroutine(OpenCharacterInformation());
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //關閉效果UI
        SmallInformationUI.readCharacterInformation = false;
    }
    public IEnumerator OpenCharacterInformation()
    {
        var playerdata = PlayerUIManager.GetInstance().PlayerData;
        SmallInformationUI.UIPos = new Vector2(transform.position.x+200, transform.position.y);
        if (TryGetComponent<EnemyData>(out var enemydata))
        {
            SmallInformationUI.TextPATK = enemydata.ATKValue.ToString();
            SmallInformationUI.TextHP = enemydata.HP.ToString();
            SmallInformationUI.TextDefense = enemydata.Defense.ToString();
        }
        else
        {
            SmallInformationUI.TextPATK = playerdata.ATKValue.ToString();
            SmallInformationUI.TextHP = playerdata.HP.ToString();
            SmallInformationUI.TextDefense = playerdata.Defense.ToString();
        }
        SmallInformationUI.readCharacterInformation = true;
        yield return null;
    }
}
