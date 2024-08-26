using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    public int coin = 0;
    private string[] coinLevel = {"", "K", "M", "B", "T" };

    private void Awake()
    {
        GameScenes.globalCoinManager = this;
    }

    private void Update()
    {

        coinText.text = $"{FormatCoin(coin)} coin";
    }

    private string FormatCoin(int value)
    {
        int i = 0;
        double displayValue = value;

        while (displayValue >= 1000 && i < coinLevel.Length - 1)
        {
            displayValue /= 1000;
            i++;
        }

        return displayValue.ToString("F1") + coinLevel[i];
    }


}
