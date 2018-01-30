using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UIWin
{

    [SerializeField] GameObject ShopItem;
    [SerializeField] GameObject ShopGameObject;
    [SerializeField] Text DetialText;
    [SerializeField] Button BuyButton;
    [SerializeField] Toggle[] Toggles;
    private BagItemBean BagItem;

    protected override void OnOpened()
    {
        base.OnOpened();
        UIManager.AddEventHandler<BagItemBean>(UIEventType.ON_CLICK_SHOPITEM, ShowBagItem);
        BuyButton.onClick.AddListener(BuyItem);
        for (int i = 0; i < Toggles.Length; i++)
            Toggles[i].onValueChanged.AddListener(OnClickMenu);

        ShowShop(0);
    }

    protected override void OnClosed()
    {
        base.OnClosed();
        UIManager.RemoveEventHandler<BagItemBean>(UIEventType.ON_CLICK_SHOPITEM, ShowBagItem);
    }

    void ShowBagItem(short evt, BagItemBean bagItem)
    {
        BagItem = bagItem;
        DetialText.text = GetText();
    }

    private string GetText()
    {
        if (BagItem == null)
            return "";
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<color=red>{0}</color>\n\n", BagItem.name);
        if (BagItem.type == 0)
                sb.AppendFormat("回复HP:{0}\n回复MP:{1}\n\n", BagItem.hp,BagItem.mp);
        else if(BagItem.type==1)
            sb.AppendFormat("攻击力+:{0}\n防御+:{1}\n\n", BagItem.atk, BagItem.def);
        sb.AppendFormat("<size=20><color=white>购买价格：{0}\n出售价格：{1}</color></size>\n\n<color=yellow><size=18>描述：{2}</size></color>", BagItem.buyPrice, BagItem.sellPrice, BagItem.description);
        return sb.ToString();
    }

    void BuyItem()
    {
        if (PlayerDataMgr.Instance.playerData.Coin >= BagItem.buyPrice)
        {
            BagDataManager.Instance.AddItem(BagItem.ID, 1, true);
            PlayerDataMgr.Instance.playerData.AddCoin(-BagItem.buyPrice);
            UIManager.Instance.ShowTips("购买成功");
        }
        else
            UIManager.Instance.Alert("金币不足","购买此商品所需的金币不足！",AlertMode.Ok);
    }

    void OnClickMenu(bool b)
    {
        if (!b) return;
        int index=0;
        for (int i = 0; i < Toggles.Length; i++)
        {
            if (Toggles[i].isOn)
            {
                index = i;
                break;
            }
        }
        ShowShop(index);
    }

    void ShowShop(int index)
    {
        int count = ShopGameObject.transform.childCount;
        while (count>0)
        {
            count--;
            DestroyImmediate(ShopGameObject.transform.GetChild(0).gameObject); 
        }
        Dictionary<int, BagItemBean> beans = BagItemBean.beans;
        foreach (var v in beans)
        {
            if(v.Value.type == index)
            {
                GameObject item = Instantiate(ShopItem);
                item.transform.SetParent(ShopGameObject.transform);
                if (!item.activeSelf)
                    item.SetActive(true);
                item.transform.localScale = Vector3.one;
                UIShopItem uiShopItem = item.GetComponent<UIShopItem>();
                uiShopItem.SetData(v.Value);
            }
        }
    }
}
