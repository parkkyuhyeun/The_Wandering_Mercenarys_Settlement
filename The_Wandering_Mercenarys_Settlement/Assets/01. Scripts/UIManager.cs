using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentCoinTxt;
    [SerializeField] TextMeshProUGUI currentHPTxt;
    [SerializeField] TextMeshProUGUI currentExpTxt;
    [SerializeField] TextMeshProUGUI currentLevelTxt;
    [SerializeField] TextMeshProUGUI dayTime;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject player;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider expSlider;

    Timer _timer;
    InventoryUI _inven;
    PlayerController _playerCon;

    public int cc;
    public int dc;
    public float maxEXP;
    public float maxHP;

    private bool day = true;

    private void Awake()
    {
        GameScenes.globalUIManager = this;
        _timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        _inven = inventory.GetComponent<InventoryUI>();
        _playerCon = player.GetComponent<PlayerController>();
    }

    private void Start()
    {
        maxHP = _playerCon.playerSO.MaxHP;
        maxEXP = _playerCon.playerSO.nextEXP;
        hpSlider.maxValue = maxHP;
    }

    private void Update()
    {
        if ( _timer.isNight && day )
        {
            dayTime.text = "Night";
            day = false;
        }
        else if ( !_timer.isNight && !day )
        {
            dayTime.text = "Day";
            day = true;
        }

        UpdateHP();
        UpdateExp();
        UpdateLevel();
    }

    public void UpdateHP()
    {
        maxEXP = _playerCon.playerSO.nextEXP;
        currentHPTxt.text = $"{_playerCon.curHP}/{maxHP}";
        hpSlider.value = _playerCon.curHP / maxHP;
    }

    public void UpdateExp()
    {
        currentExpTxt.text = $"{expSlider.value}/{maxEXP}";
        expSlider.value = _playerCon.playerSO.curEXP;
    }

    public void UpdateLevel()
    {
        currentLevelTxt.text = $"Level. {_playerCon.playerSO.Level}";
    }
}
