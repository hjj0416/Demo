using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDataMgr : BaseDataMgr<EquipmentDataMgr> {
    private EquipmentData _equipmentData;
    public EquipmentData equipmentData
    {
        get
        {
            if (_equipmentData == null)
                _equipmentData = data as EquipmentData;
            return _equipmentData;
        }
    }

    private const string EQUIPMENT_FILE_PATH = "Equipment.txt";

    public override void Init()
    {
        base.Init();
        file_path = EQUIPMENT_FILE_PATH;
        Read(typeof(EquipmentData));
    }

    public void Dress(BagItemBean bagItem)
    {
        if(bagItem.part==1)
        {
            if (equipmentData.weapon != 0)
                BagDataManager.Instance.AddItem(equipmentData.weapon, 1, true);
            equipmentData.weapon = bagItem.ID;
        }
        if(bagItem.part==2)
        {
            if (equipmentData.helment != 0)
                BagDataManager.Instance.AddItem(equipmentData.helment, 1, true);
            equipmentData.helment = bagItem.ID;
        }
        if (bagItem.part == 3)
        {
            if (equipmentData.clothes != 0)
                BagDataManager.Instance.AddItem(equipmentData.clothes, 1, true);
            equipmentData.clothes = bagItem.ID;
        }
        if (bagItem.part == 4)
        {
            if (equipmentData.pants != 0)
                BagDataManager.Instance.AddItem(equipmentData.pants, 1, true);
            equipmentData.pants = bagItem.ID;
        }
        if (bagItem.part == 5)
        {
            if (equipmentData.shoes != 0)
                BagDataManager.Instance.AddItem(equipmentData.shoes, 1, true);
            equipmentData.shoes = bagItem.ID;
        }
        if (bagItem.part == 6)
        {
            if (equipmentData.necklace != 0)
                BagDataManager.Instance.AddItem(equipmentData.necklace, 1, true);
            equipmentData.necklace = bagItem.ID;
        }
        if (bagItem.part == 7)
        {
            if (equipmentData.ring != 0)
                BagDataManager.Instance.AddItem(equipmentData.ring, 1, true);
            equipmentData.ring = bagItem.ID;
        }
        Save();
        MarkDirty();
    }

    public void UnDress(BagItemBean bagItem)
    {
        if(bagItem.part==1)
            equipmentData.weapon = 0;
        else if (bagItem.part == 2)
            equipmentData.helment = 0;
        else if (bagItem.part == 3)
            equipmentData.clothes = 0;
        else if (bagItem.part == 4)
            equipmentData.pants = 0;
        else if (bagItem.part == 5)
            equipmentData.shoes = 0;
        else if (bagItem.part == 6)
            equipmentData.necklace = 0;
        else if (bagItem.part == 7)
            equipmentData.ring = 0;
    
        Save();
        BagDataManager.Instance.AddItem(bagItem.ID,1,true);
        MarkDirty();
    }
}
