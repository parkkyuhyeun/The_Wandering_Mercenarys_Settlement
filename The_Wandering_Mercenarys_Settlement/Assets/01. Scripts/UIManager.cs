using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayTime;
    [SerializeField] GameObject inventory;

    Timer _timer;
    InventoryUI _inven;

    public int cc;
    public int dc;

    private bool day = true;

    private void Awake()
    {
        _timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        _inven = inventory.GetComponent<InventoryUI>();

        for (int i = 0; i < 10; i++)
        {
            _inven.currentItem.Add(0);
        }
        for (int i = 0; i < 10; i++)
        {
            _inven.currentPotion.Add(0);
        }
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
    }
}
