using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyGemItem : MonoBehaviour {

    [SerializeField] Button BuyButton;
    [SerializeField] Text Text;
    [SerializeField] int Count;

	// Use this for initialization
	void Start () {
        BuyButton.onClick.AddListener(OnClick);
        Text.text = string.Format("{0}钻石",Count);
	}
	
    void OnClick()
    {
        Debug.Log("购买钻石："+Count);
        PlayerDataMgr.Instance.playerData.AddGem(Count);
    }
}
