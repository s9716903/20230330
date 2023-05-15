using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageDropText : MonoBehaviour
{
    public GameObject Player;
    private int NeedDropValue;
    // Update is called once per frame
    void Update()
    {
        NeedDropValue = Player.GetComponent<Player>().AllDamaged;
        GetComponent<TextMeshProUGUI>().text = "Choose " + NeedDropValue + " Card Drop";
    }
}
