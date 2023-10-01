using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandCardManager : MonoBehaviour
{
    public List<Card> HandCardList; //該玩家手牌(List)
    public GameObject PlayerDeck; //該玩家牌組
    public GameObject PlayerPiece; //該玩家棋子
    public GameObject PlayerTrashCardZone; //該玩家棄牌區
    private GridLayoutGroup gridLayoutGroup; //HandCardZone Group

    //public bool PracticeLimited;
    // Start is called before the first frame update
    void Start()
    {
        //PracticeLimited = false;
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (PracticeLimited == false)
        {
            for (int b = 0; b < transform.childCount; b++) //判斷底下的卡片是否能與滑鼠互動
            {
                var handcard = transform.GetChild(b);
                if (ThisPlayer.GetComponent<Player>().isReady == false && ThisPlayer.GetComponent<Player>().canMove == false && ThisPlayer.name == "Player")
                {
                    handcard.GetComponent<CardManager>().isPlayerUse = true;
                }
                else
                {
                    handcard.GetComponent<CardManager>().isPlayerUse = false;
                }
            }
        }
        else
        {
            for (int b = 0; b < transform.childCount; b++) //判斷底下的卡片是否能與滑鼠互動
            {
                var handcard = transform.GetChild(b);
                if (b < PracticeLimtedSetting.LimitedUseHowManyCard)
                {
                    handcard.GetComponent<CardManager>().isPlayerUse = true;
                }
                else
                {
                    handcard.GetComponent<CardManager>().isPlayerUse = false;
                }
            }
        }*/
        //手牌越多時縮減每張牌的間距
        if (gridLayoutGroup.cellSize.x <= 70)
        {
            gridLayoutGroup.cellSize = new Vector2(70, 100);
        }
        else
        {
            gridLayoutGroup.cellSize = new Vector2(130 - (transform.childCount * 5), 100);
        }
    }
    /*public IEnumerator TrashCardBackDeck()
    {

        var trashcard = ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject[0];
        Instantiate(trashcard, ThisTrashCardZone.transform); //卡片變成棄牌區子物件
        var thistrashcard = ThisTrashCardZone.transform.GetChild(1).gameObject;
        while (thistrashcard.transform.position != PlayerDeck.transform.position)
        {
            thistrashcard.transform.position = Vector3.MoveTowards(thistrashcard.transform.position, PlayerDeck.transform.position, 300 * Time.deltaTime);
            yield return 0;
        }
        PlayerDeck.GetComponent<Deck>().DeckAllCard = ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.GetRange(0, ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.Count); //卡牌加入牌組List
        PlayerDeck.GetComponent<Deck>().DeckImage.enabled = true;
        ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.RemoveRange(0, ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.Count); //牌組List中移除卡牌
        Destroy(thistrashcard);
        yield return new WaitForSeconds(0);
        yield return 0;
        PlayerDeck.GetComponent<Deck>().isDeckNull = false;
    }*/
}
