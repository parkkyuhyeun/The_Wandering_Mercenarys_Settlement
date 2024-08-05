using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayTime;

    Timer _timer;

    private bool day = true;

    private void Awake()
    {
        _timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
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
