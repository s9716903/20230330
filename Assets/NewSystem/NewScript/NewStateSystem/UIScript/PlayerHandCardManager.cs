using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandCardManager : MonoBehaviour
{
    public GameObject PlayerDeck; //�Ӫ��a�P��
    public GameObject PlayerPiece; //�Ӫ��a�Ѥl
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
