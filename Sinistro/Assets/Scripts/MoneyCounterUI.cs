﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyCounterUI : MonoBehaviour {

    private Text moneyText;

    private void Awake()
    {
        moneyText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () 
    {
        moneyText.text = "Money: " + GameMaster.Money.ToString();	
	}
}
