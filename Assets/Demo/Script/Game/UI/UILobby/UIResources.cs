using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResources : MonoBehaviour {

    [SerializeField] Button CoinButton;
    [SerializeField] Button GemButton;
    [SerializeField] Button EnergyButton;
    [SerializeField] Button SettingButton;

    [SerializeField] Slider EnergySlider;

    [SerializeField] Text EnergyText;
    [SerializeField] Text CoinText;
    [SerializeField] Text GemText;

    [SerializeField] int EnergyCount;
    [SerializeField] int CoinCount;
     void Start()
    {
        InitView();
        CoinButton.onClick.AddListener(OnClickCoin);
        GemButton.onClick.AddListener(OnClickGem);
        EnergyButton.onClick.AddListener(OnClickEnergy);
        SettingButton.onClick.AddListener(OnClickSetting);

        PlayerDataMgr.Instance.dispatcher.AddEventHandler<int>(PlayerDataEvent.COIN_CHANGED, OnCoinChanged);
        PlayerDataMgr.Instance.dispatcher.AddEventHandler<int>(PlayerDataEvent.GEM_CHANGED, OnGemChanged);
        PlayerDataMgr.Instance.dispatcher.AddEventHandler<int>(PlayerDataEvent.ENERGY_CHANGED, OnEnergyChanged);
    }

    private void OnDestroy()
    {
        PlayerDataMgr.Instance.dispatcher.RemoveEventHandler<int>(PlayerDataEvent.COIN_CHANGED, OnCoinChanged);
        PlayerDataMgr.Instance.dispatcher.RemoveEventHandler<int>(PlayerDataEvent.GEM_CHANGED,OnGemChanged);
        PlayerDataMgr.Instance.dispatcher.RemoveEventHandler<int>(PlayerDataEvent.ENERGY_CHANGED,OnEnergyChanged);
    }


    void InitView()
    {
        var playerData = PlayerDataMgr.Instance.playerData;
        if (playerData == null)
            return;
        EnergyText.text = string.Format("{0}/150", playerData.Energy);
        EnergySlider.value = playerData.EnergySlider;
        CoinText.text = playerData.Coin.ToString();
        GemText.text = playerData.Gem.ToString();    
    }


    void OnClickCoin()
    {
        UIManager.Instance.ShowWindow("UIBuyCoin");
    }

    void OnClickGem()
    {
        UIManager.Instance.ShowWindow("UIBuyGem");
    }

    void OnClickEnergy()
    {
        PlayerDataMgr.Instance.playerData.AddEnergy(EnergyCount, CoinCount);
    }

    void OnClickSetting()
    {
        UIManager.Instance.ShowWindow("UISetting");
    }


    void OnCoinChanged(short evt, int count)
    {
        CoinText.text = count.ToString();
    }

    void OnGemChanged(short evt,int count)
    {
        GemText.text = count.ToString();
    }

    void OnEnergyChanged(short evt,int count)
    {
        EnergySlider.value = (float)count / 150;
        EnergyText.text = string.Format("{0}/150",count);
        
    }
}
