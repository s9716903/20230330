using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandCardManager : MonoBehaviour
{
    public PlayerDataManager playerdata;
    public List<Card> HandCardList; //�Ӫ��a��P(List)
    public List<Card> ReadyCardList; //�ǳƨϥΪ���P(List)
    public GameObject PlayerDeck; //�Ӫ��a�P��
    public GameObject PlayerPiece; //�Ӫ��a�Ѥl
    public GameObject PlayerTrashCardZone; //�Ӫ��a��P��
    private GridLayoutGroup gridLayoutGroup; //HandCardZone Group

    //public bool PracticeLimited;
    // Start is called before the first frame update
    void Start()
    {
        //PracticeLimited = false;
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        //StartCoroutine(NormalDraw());
    }

    // Update is called once per frame
    void Update()
    {      
        //playerdata.HP = HandCardList.Count;
        /*if (PracticeLimited == false)
        {
            for (int b = 0; b < transform.childCount; b++) //�P�_���U���d���O�_��P�ƹ�����
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
            for (int b = 0; b < transform.childCount; b++) //�P�_���U���d���O�_��P�ƹ�����
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
        //��P�V�h���Y��C�i�P�����Z
        if (gridLayoutGroup.cellSize.x <= 70)
        {
            gridLayoutGroup.cellSize = new Vector2(70, 100);
        }
        else
        {
            gridLayoutGroup.cellSize = new Vector2(130 - (transform.childCount * 5), 100);
        }
    }
    /*public void ShowHandCard()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                Destroy(transform.GetChild(i).gameObject);
                //ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.Add(HandAllCard[i]);//��P�[�J��PList
                //HandAllCard[i] = null; //�P��List�������d�P
            }
            else if (!transform.GetChild(i).gameObject.activeInHierarchy)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        //HandAllCard.RemoveAll(HandAllCard => HandAllCard == null);
    }*/
    /*public IEnumerator TrashCardBackDeck()
    {

        var trashcard = ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject[0];
        Instantiate(trashcard, ThisTrashCardZone.transform); //�d���ܦ���P�Ϥl����
        var thistrashcard = ThisTrashCardZone.transform.GetChild(1).gameObject;
        while (thistrashcard.transform.position != PlayerDeck.transform.position)
        {
            thistrashcard.transform.position = Vector3.MoveTowards(thistrashcard.transform.position, PlayerDeck.transform.position, 300 * Time.deltaTime);
            yield return 0;
        }
        PlayerDeck.GetComponent<Deck>().DeckAllCard = ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.GetRange(0, ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.Count); //�d�P�[�J�P��List
        PlayerDeck.GetComponent<Deck>().DeckImage.enabled = true;
        ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.RemoveRange(0, ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.Count); //�P��List�������d�P
        Destroy(thistrashcard);
        yield return new WaitForSeconds(0);
        yield return 0;
        PlayerDeck.GetComponent<Deck>().isDeckNull = false;
    }*/
}
