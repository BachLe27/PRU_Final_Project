using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public TextMeshProUGUI monsterCount;
    // Start is called before the first frame update
    void Start()
    {
        monsterCount = GameObject.Find("MonsterCountTxt").GetComponent<TextMeshProUGUI>();
        PlayerPrefs.SetInt("TotalMonsterDestroyed", 0);
    }

    // Update is called once per frame
    void Update()
    {
        monsterCount.text = String.Format("{0:000}",PlayerPrefs.GetInt("TotalMonsterDestroyed", 0));
    }
}
