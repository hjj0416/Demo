using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipmentItem : MonoBehaviour {
    [SerializeField] Image Image;
    [SerializeField] Button Button;
    public enumEquipment position;
    private BagItemBean bagItem;
    
    private void Start()
    {
        Button.onClick.AddListener(OnClick);
        EquipmentDataMgr.Instance.dispatcher.AddEventHandler<BagItemBean>(EquimentDataEvent.EQUUIMENT_CHANGED, UpdateImage);
    }
    private void OnDestroy()
    {
        EquipmentDataMgr.Instance.dispatcher.RemoveEventHandler<BagItemBean>(EquimentDataEvent.EQUUIMENT_CHANGED, UpdateImage);
    }

    public void UpdateImage(short evt, BagItemBean item)
    {
        bagItem = item;
        if ((int)position == item.part)
        {
            if (!Image.gameObject.activeSelf)
                Image.gameObject.SetActive(true);
            Image.sprite = Resources.Load<Sprite>(item.icon);
        }
    }

    public void OnClick()
    {
        if (bagItem == null)
            return;
        EquipmentDataMgr.Instance.UnDress(bagItem);
        if (Image.gameObject.activeSelf)
            Image.gameObject.SetActive(false);
    }
}
