using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterPiece : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //���X�ĪGUI
        if ((PlayerUIManager.GetInstance().PlayerData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerActivate))
        {
            StartCoroutine(OpenCharacterInformation());
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //�����ĪGUI
        SmallInformationUI.readCharacterInformation = false;
    }
    public IEnumerator OpenCharacterInformation()
    {
        var playerdata = PlayerUIManager.GetInstance().PlayerData;
        SmallInformationUI.UIPos = transform.position;
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
