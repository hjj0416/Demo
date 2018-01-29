using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBagClickMenu : MonoBehaviour {

    [SerializeField] Button useBtn;
    [SerializeField] Button sellBtn;
    [SerializeField] Button Button;
    private BagItemBean bagItem;
    public int itemNum;//所拥有该道具的数量
    public int sellNum;//需要卖出的数量
	

	void Start () {
        BagDataManager.AddEventHandler<UIItem>(BagDataEvent.CLICK_ITEM,ClickItem);
        useBtn.onClick.AddListener(UseBtnClick);
        sellBtn.onClick.AddListener(SellBtnClick);
        Button.onClick.AddListener(OnSell);
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        BagDataManager.RemoveEventHandler<UIItem>(BagDataEvent.CLICK_ITEM, ClickItem);
    }

    void Update () {
		
	}

    void ClickItem(short evt,UIItem item)
    {
        bagItem = item.item;
        itemNum = item.ItemNum;
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
       // GameObject.Find("BagMenubg").transform.position= item.transform.position;
        gameObject.transform.GetChild(0).position = item.transform.position;
    }
    void UseBtnClick()
    {
        if (bagItem.type == 1)
        {
            EquipmentDataMgr.Instance.Dress(bagItem);
            EquipmentDataMgr.Instance.dispatcher.SendEvent<BagItemBean>(EquimentDataEvent.EQUUIMENT_CHANGED, bagItem);

        }

        if (bagItem.type==0)
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroController>();
            player.GetHeal(bagItem.hp);
        }

        PlayerDataMgr.Instance.Save();
        BagDataManager.Instance.DeleteItem(bagItem.ID, 1, true);
        gameObject.SetActive(false);
    }

    void SellBtnClick()
    {
        if (itemNum == 1)
        {
            sellNum = 1;
            OnSell();
        }     
        if(itemNum>1)
        {
            BagDataManager.SendEvent<UIBagClickMenu>(BagDataEvent.SELL_NUM, this);
        }
    }

    void OnSell()
    {
        UIManager.Instance.Alert("出售", string.Format("出售可获得{0}金币，确定出售吗？", bagItem.sellPrice*sellNum), AlertMode.OkCancel, SellItem);
    }

    void SellItem(AlertResult result)
    {
        if(result==AlertResult.OK)
        {
            int getCoin = bagItem.sellPrice * sellNum;
            PlayerDataMgr.Instance.playerData.AddCoin(getCoin);
            BagDataManager.Instance.DeleteItem(bagItem.ID,sellNum,true);
            gameObject.SetActive(false);
            UIManager.Instance.ShowTips(string.Format("获得{0}金币",getCoin));
        }
    }
}
