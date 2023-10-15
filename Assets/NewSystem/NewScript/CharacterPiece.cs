using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterPiece : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //跳出效果UI
        if ((PlayerUIManager.GetInstance().PlayerData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerActivate))
        {
            StartCoroutine(OpenCharacterInformation());
        }
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        //關閉效果UI
        SmallInformationUI.readCharacterInformation = false;
    }
    public IEnumerator OpenCharacterInformation()
    {
        var playerdata = PlayerUIManager.GetInstance().PlayerData;
        SmallInformationUI.UIPos = transform.position;
        if (TryGetComponent<EnemyData>(out var enemydata))
        {
            SmallInformationUI.TextPATK = enemydata.PhysicATKValue.ToString();
            SmallInformationUI.TextMATK = enemydata.MagicATKValue.ToString();
            SmallInformationUI.TextHP = enemydata.HP.ToString();
            SmallInformationUI.TextDefense = enemydata.Defense.ToString();
            SmallInformationUI.TextMove = 0.ToString();
        }
        else
        {
            SmallInformationUI.TextPATK = playerdata.PhysicATK.ToString();
            SmallInformationUI.TextMATK = playerdata.MagicATK.ToString();
            SmallInformationUI.TextHP = playerdata.HP.ToString();
            SmallInformationUI.TextDefense = playerdata.Defense.ToString();
            SmallInformationUI.TextMove = playerdata.MoveValue.ToString();
        }
        SmallInformationUI.readCharacterInformation = true;
        yield return null;
    }
}
