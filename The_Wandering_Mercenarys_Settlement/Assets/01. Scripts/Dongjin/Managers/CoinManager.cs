using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [System.Serializable]
    public class CoinWithLevel
    {
        public double[] coinContain = { 0, 0, 0, 0, 0 };
        public string[] coinLevel = { "", "k", "m", "b", "t" };
        public int levelCnt = 0;
    }

    [SerializeField] private TextMeshProUGUI coinText;
    public CoinWithLevel coin = new CoinWithLevel();

    private void Awake()
    {
        coin.coinLevel = new string[]{ "", "k", "m", "b", "t" };
        coin.levelCnt = 0;
        coin.coinContain = new double[] { 0, 0, 0, 0, 0 };
        GameScenes.globalCoinManager = this;
        UpdateCoin();
        Debug.Log($"");
    }

    public string FormatCoin(CoinWithLevel value)
    {
        int levelCnt = value.levelCnt;
        double displayValue = value.coinContain[levelCnt];

        while (displayValue >= 1000 && levelCnt < value.coinLevel.Length - 1)
        {
            displayValue /= 1000;
            levelCnt++;
        }

        return displayValue.ToString("F2") + value.coinLevel[levelCnt];
    }


    public void UpdateCoin()
    {
        if (coin.coinContain[coin.levelCnt] >= 1000)
            coinText.text = $"{FormatCoin(coin)} coin";
        else
        {
            Debug.Log($"코인 컨테인: {coin.coinContain}\n단위 컨테인: {coin.coinLevel}\n레벨: {coin.levelCnt}");
            coinText.text = $"{coin.coinContain[coin.levelCnt].ToString("F2")}{coin.coinLevel[coin.levelCnt]} coin";
        }
    }

}
