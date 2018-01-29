using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipment : UIWin {

    [SerializeField] UIEquipmentItem weapon;
    [SerializeField] UIEquipmentItem helmet;
    [SerializeField] UIEquipmentItem clothes;
    [SerializeField] UIEquipmentItem pants;
    [SerializeField] UIEquipmentItem shoes;
    [SerializeField] UIEquipmentItem necklace;
    [SerializeField] UIEquipmentItem ring;

    private void Start()
    {
        OnDataChanged(0);
    }

    protected override void OnOpened()
    {
        base.OnOpened();

        EquipmentDataMgr.Instance.dispatcher.AddEventHandler(EquimentDataEvent.DATA_CHANGED, OnDataChanged);
    }

    protected override void OnClosed()
    {
        base.OnClosed();
        EquipmentDataMgr.Instance.dispatcher.RemoveEventHandler(EquimentDataEvent.DATA_CHANGED, OnDataChanged);
    }

    void OnDataChanged(short evt)
    {
        Dictionary<int, BagItemBean> beans = BagItemBean.beans;
        BagItemBean temp;
        if (beans.TryGetValue(EquipmentDataMgr.Instance.equipmentData.weapon, out temp))
            weapon.UpdateImage(0, temp);
        if(beans.TryGetValue(EquipmentDataMgr.Instance.equipmentData.helment, out temp))
            helmet.UpdateImage(0, temp);
        if(beans.TryGetValue(EquipmentDataMgr.Instance.equipmentData.clothes, out temp))
            clothes.UpdateImage(0, temp);
        if(beans.TryGetValue(EquipmentDataMgr.Instance.equipmentData.pants, out temp))
            pants.UpdateImage(0, temp);
        if(beans.TryGetValue(EquipmentDataMgr.Instance.equipmentData.shoes, out temp))
            shoes.UpdateImage(0, temp);
        if(beans.TryGetValue(EquipmentDataMgr.Instance.equipmentData.necklace, out temp))
            necklace.UpdateImage(0, temp);
        if(beans.TryGetValue(EquipmentDataMgr.Instance.equipmentData.ring, out temp))
            ring.UpdateImage(0, temp);
    }
}
