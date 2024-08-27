using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject itemTab;
    [SerializeField] GameObject potionTab;
    [SerializeField] GameObject[] informationTabs;
    [SerializeField] GameObject[] soldOutPanel;
    [SerializeField] GameObject[] gettingItem;

    [SerializeField] TextMeshProUGUI currentCoinTxt;
    [SerializeField] GameObject uimanager;

    UIManager ui;

    int currentItemNum = 0;

    int[] haveArrays;
    int[] isSoldOuts;

    private void Awake()
    {
        ui = uimanager.GetComponent<UIManager>();
    }
    private void Start()
    {
        var coinPack = GameScenes.globalCoinManager.coin;
        currentCoinTxt.text = $"{coinPack.coinContain[coinPack.levelCnt]} Coin";
        isSoldOuts = new int[informationTabs.Length];
        haveArrays = new int[informationTabs.Length];

        //갯수 제한 설정
        haveArrays[0] = 1;
        haveArrays[1] = 1;
        haveArrays[2] = 1;
        haveArrays[3] = 1;
        haveArrays[4] = 1;
        haveArrays[5] = 5;
        haveArrays[6] = 5;
    }
    public void ChangeTab()
    {
        itemTab.SetActive(!itemTab.activeSelf);
        potionTab.SetActive(!potionTab.activeSelf);
    }

    public void CloseUI()
    {
        shopUI.SetActive(false);
    }

    public void OpenInformation(int num)
    {
        for(int i = 0; i < informationTabs.Length; i++)
        {
            informationTabs[i].SetActive(false);
        }
        informationTabs[num].SetActive(true);
        currentItemNum = num;
    }

    public void Buy(int cost,int costLevel)
    {
        var coinPack = GameScenes.globalCoinManager.coin;
        if (coinPack.coinContain[coinPack.levelCnt] >= cost && isSoldOuts[currentItemNum] != 1)
        {
            currentCoinTxt.text = $"{coinPack.coinContain[costLevel] - cost}{coinPack.coinLevel[costLevel]} Coin";
            coinPack.coinContain[costLevel] -= cost;
            haveArrays[currentItemNum]--;

            if (haveArrays[currentItemNum] == 0)
            {
                isSoldOuts[currentItemNum] = 1;
            }
        }
    }

    public void SoldOut(int num)
    {
        if (isSoldOuts[num] == 1)
        {
            soldOutPanel[num].SetActive(true);
        }
    }

    public void GetItem(int num)
    {
        gettingItem[num].SetActive(true);
    }
}
