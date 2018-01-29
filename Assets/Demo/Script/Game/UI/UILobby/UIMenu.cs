using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour {

    [SerializeField] Button BagButton;
    [SerializeField] Button ShopButton;

    private void Start()
    {
        BagButton.onClick.AddListener(BagOnClick);
        ShopButton.onClick.AddListener(ShopClick);
    }

    void BagOnClick()
    {
        UIManager.Instance.ShowWindow("UIBag");
        UIManager.Instance.ShowWindow("UIEquipment");
    }

    void ShopClick()
    {
        UIManager.Instance.ShowWindow("UIShop");
    }
}
