using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour {

    [SerializeField] Image ImageIcon;
    [SerializeField] Button Button;
    private BagItemBean BagItem;

    public void SetData(BagItemBean bagItem)
    {
        BagItem = bagItem;
        ImageIcon.sprite = Resources.Load<Sprite>(bagItem.icon);
        Button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        UIManager.SendEvent<BagItemBean>(UIEventType.ON_CLICK_SHOPITEM, BagItem);
    }
}
