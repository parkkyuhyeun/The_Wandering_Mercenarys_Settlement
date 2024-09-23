using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [System.Serializable]
    public class CoinWithLevel : IComparable<CoinWithLevel>
    {
        public float[] coinContain = { 0, 0, 0, 0, 0 };
        public string[] coinLevel = { "", "k", "m", "b", "t" };
        public int levelCnt = 0;

        // 코인을 실제 값으로 변환
        private float GetCoinValue()
        {
            // 코인 레벨에 따라 값을 1000, 1000000, 1000000000 등으로 변환
            float multiplier = (float)Mathf.Pow(1000, levelCnt);
            return coinContain[levelCnt] * multiplier;
        }

        // IComparable<T> 인터페이스 구현
        public int CompareTo(CoinWithLevel other)
        {
            if (other == null) return 1;

            // 현재 객체와 비교 객체의 값을 비교
            float thisValue = GetCoinValue();
            float otherValue = other.GetCoinValue();

            return thisValue.CompareTo(otherValue);
        }

        // Equals 오버라이드
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            CoinWithLevel other = (CoinWithLevel)obj;
            return GetCoinValue() == other.GetCoinValue();
        }

        // GetHashCode 오버라이드
        public override int GetHashCode()
        {
            return GetCoinValue().GetHashCode();
        }

        // 연산자 오버로딩
        public static bool operator >(CoinWithLevel c1, CoinWithLevel c2)
        {
            return c1.CompareTo(c2) > 0;
        }

        public static bool operator <(CoinWithLevel c1, CoinWithLevel c2)
        {
            return c1.CompareTo(c2) < 0;
        }

        public static bool operator >=(CoinWithLevel c1, CoinWithLevel c2)
        {
            return c1.CompareTo(c2) >= 0;
        }

        public static bool operator <=(CoinWithLevel c1, CoinWithLevel c2)
        {
            return c1.CompareTo(c2) <= 0;
        }
    }

    [SerializeField] private TextMeshProUGUI coinText;
    public CoinWithLevel coin = new CoinWithLevel();

    private void Awake()
    {
        GameScenes.globalCoinManager = this;
        UpdateCoin(0);
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
