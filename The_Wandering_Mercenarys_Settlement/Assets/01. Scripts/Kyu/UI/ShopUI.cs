using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [System.Serializable]
    public class BuyButton
    {
        public Button button;
        public int cost;
        public int costLevel;
        public ObjectType.WeaponType type;
    }


    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject itemTab;
    [SerializeField] GameObject potionTab;
    [SerializeField] GameObject[] informationTabs;
    [SerializeField] GameObject[] soldOutPanel;
    [SerializeField] GameObject[] gettingItem;
    [SerializeField] List<BuyButton> buttons; // 버튼 컴포넌트를 가져옴

    [SerializeField] TextMeshProUGUI currentCoinTxt;
    [SerializeField] GameObject uimanager;

    

    UIManager ui;

    int currentItemNum = 0;

    int[] haveArrays;
    int[] isSoldOuts;

    private void Awake()
    {
        ui = uimanager.GetComponent<UIManager>();
        foreach(var btn in buttons)
        {
            btn.button.onClick.AddListener(() => Buy(btn.cost, btn.costLevel, btn.type));
        }
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

    public void Buy(int cost,int costLevel, ObjectType.WeaponType type)
    {
        CoinManager.CoinWithLevel newCoin = new CoinManager.CoinWithLevel();
        newCoin.levelCnt = costLevel;
        newCoin.coinContain[costLevel] = cost;
        if(GameScenes.globalCoinManager.coin < newCoin)
        {
            Debug.Log("코인 부족");
            return;
        }
        if (GameScenes.globalCoinManager.coin.coinContain[GameScenes.globalCoinManager.coin.levelCnt] >= cost && isSoldOuts[currentItemNum] != 1)
        {
            Debug.Log($"{type.ToString()} 구매");
            currentCoinTxt.text = $"{GameScenes.globalCoinManager.coin.coinContain[costLevel] - cost}{GameScenes.globalCoinManager.coin.coinLevel[costLevel]} Coin";
            GameScenes.globalCoinManager.coin.coinContain[costLevel] -= cost;
            haveArrays[currentItemNum]--;
            GameScenes.globalPlayerController.SpawnWeapon(type);
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
