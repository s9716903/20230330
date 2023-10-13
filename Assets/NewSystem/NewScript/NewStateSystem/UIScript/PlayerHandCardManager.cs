using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandCardManager : MonoBehaviour
{
    public GameObject PlayerDeck; //該玩家牌組
    public GameObject PlayerPiece; //該玩家棋子
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
        /*if (transform.childCount > 8)
        {
            if (gridLayoutGroup.cellSize.x <= 60)
            {
                gridLayoutGroup.cellSize = new Vector2(60, 20);
            }
            else
            {
                gridLayoutGroup.cellSize = new Vector2(120 - (10 * (transform.childCount - 8)), 20);
            }
        }
        else
        {
            gridLayoutGroup.cellSize = new Vector2(120, 40);
        }*/
    }
}
