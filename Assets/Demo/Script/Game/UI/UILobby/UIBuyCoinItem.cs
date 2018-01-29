using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyCoinItem : MonoBehaviour {

    [SerializeField] Button BuyButton;
    [SerializeField] Text NameText;
    [SerializeField] int Count;

    private void Start()
    {
        BuyButton.onClick.AddListener(OnClick);
        NameText.text = string.Format("{0}金币",Count);
    }

    void OnClick()
    {
        Debug.Log("购买金币"+Count);
        PlayerDataMgr.Instance.playerData.AddCoin(Count);
    }
}
