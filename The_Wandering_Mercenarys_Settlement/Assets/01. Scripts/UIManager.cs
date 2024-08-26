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
    public int currentCoin = 1000;
    public float maxEXP;
    public float maxHP;

    private bool day = true;

    private void Awake()
    {
        _timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        _inven = inventory.GetComponent<InventoryUI>();
        _playerCon = player.GetComponent<PlayerController>();

        for (int i = 0; i < 10; i++)
        {
            _inven.currentItem.Add(0);
        }
        for (int i = 0; i < 10; i++)
        {
            _inven.currentPotion.Add(0);
        }
    }

    private void Start()
    {
        maxHP = _playerCon.curHP;
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

        ChangeCurrentCoinTxt();
        UpdateHP();
        UpdateExp();
        UpdateLevel();
    }

    public void ChangeCurrentCoinTxt()
    {
        currentCoinTxt.text = $"{currentCoin} Coin";
    }

    public void UpdateHP()
    {
        maxEXP = _playerCon.playerSO.nextEXP;
        currentHPTxt.text = $"{hpSlider.value}/{maxHP}";
        hpSlider.value = _playerCon.curHP;
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
