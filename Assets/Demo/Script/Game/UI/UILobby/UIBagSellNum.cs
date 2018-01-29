using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIBagSellNum : MonoBehaviour
{

    [SerializeField] Button button;
    [SerializeField] InputField sellNum;
    private UIBagClickMenu uIBagClickMenu;
    // Use this for initialization
    void Start()
    {
        BagDataManager.AddEventHandler<UIBagClickMenu>(BagDataEvent.SELL_NUM,SellItem);
        button.onClick.AddListener(OnClick);
        sellNum.onValueChanged.AddListener(OnSellNumChanged);
        sellNum.keyboardType = TouchScreenKeyboardType.NamePhonePad;
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        BagDataManager.RemoveEventHandler<UIBagClickMenu>(BagDataEvent.SELL_NUM, SellItem);
        button.onClick.RemoveListener(OnClick);
        sellNum.onValueChanged.RemoveListener(OnSellNumChanged);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SellItem(short evt, UIBagClickMenu menu)
    {
        uIBagClickMenu = menu;
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        gameObject.transform.GetChild(0).position = menu.transform.GetChild(0).position;
    }

    void OnSellNumChanged(string value)
    {
        int num = 1;
        if (string.IsNullOrEmpty(value))
            sellNum.text ="1";
        else
            num = Convert.ToInt32(value);

        //使输入框内的值在1和拥有数量之间
        num = num < 1 ? 1 : num = num > uIBagClickMenu.itemNum ? uIBagClickMenu.itemNum : num;

        uIBagClickMenu.sellNum = num;
        if ( !string.IsNullOrEmpty(value)&&Convert.ToInt32(value) != num)
            sellNum.text = num.ToString();
    }

    void OnClick()
    {
        gameObject.SetActive(false);
    }
}
