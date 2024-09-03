using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [System.Serializable]
    public class CoinWithLevel
    {
        public float[] coinContain = { 0, 0, 0, 0, 0 };
        public string[] coinLevel = { "", "k", "m", "b", "t" };
        public int levelCnt = 0;
    }

    [SerializeField] private TextMeshProUGUI coinText;
    public CoinWithLevel coin = new CoinWithLevel();

    private void Awake()
    {
        coin.coinLevel = new string[]{ "", "k", "m", "b", "t" };
        coin.levelCnt = 0;
        coin.coinContain = new float[] { 100f, 0f, 0f, 0f, 0f };
        GameScenes.globalCoinManager = this;
        UpdateCoin(0);
        Debug.Log($"");
    }

    public string FormatCoin(int levelCnt)
    {
        if(levelCnt < coin.levelCnt)
        {
            while (coin.coinContain[levelCnt] >= 1000 && levelCnt < coin.coinLevel.Length - 1)
            {
                coin.coinContain[levelCnt] /= 1000f;
                levelCnt++;
                coin.coinContain[levelCnt] += coin.coinContain[levelCnt - 1];
                if (coin.coinContain[levelCnt] < 1000f) break;
            }
        }
        else if(levelCnt >= coin.levelCnt)
        {
            while (coin.coinContain[coin.levelCnt] >= 1000 && coin.levelCnt < coin.coinLevel.Length - 1)
            {
                coin.coinContain[coin.levelCnt] /= 1000f;
                coin.levelCnt++;
                coin.coinContain[coin.levelCnt] += coin.coinContain[coin.levelCnt - 1];
                if (coin.coinContain[coin.levelCnt] < 1000f) break;
            }
        }
        return (Mathf.Floor(coin.coinContain[coin.levelCnt] * 100) / 100).ToString() + coin.coinLevel[coin.levelCnt];

    }


    public void UpdateCoin(int levelCnt)
    {
        if (coin.coinContain[levelCnt] >= 1000)
            coinText.text = $"{FormatCoin(levelCnt)} coin";
        else
        {
            Debug.Log($"코인 컨테인: {coin.coinContain[0]}, {coin.coinContain[1]}, {coin.coinContain[2]}, {coin.coinContain[3]}, {coin.coinContain[4]}\n단위 컨테인: {coin.coinLevel[0]}, {coin.coinLevel[1]}, {coin.coinLevel[2]}, {coin.coinLevel[3]}, {coin.coinLevel[4]}\n레벨: {coin.levelCnt}");
            coinText.text = $"{Mathf.Floor(coin.coinContain[coin.levelCnt] * 100) / 100}{coin.coinLevel[coin.levelCnt]} coin";
        }
    }

}
