using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BagDataManager : BaseDataMgr<BagDataManager>
{

    private BagData _bagData;
    public BagData bagData
    {
        get {
            if (_bagData == null)
                _bagData = data as BagData;
            return _bagData;
        }
    }


    private const string BAGITEM_FILE_PATH = "BagItems.txt";

    public override void Init()
    {
        base.Init();
        file_path = BAGITEM_FILE_PATH;
        Read(typeof(BagData));
    }


    public void AddItem(int id,int num, bool autoSave = false)
    {
        bool find = false;
        for (int i = 0; i < bagData.items.Count; i++)
        {
            var item = bagData.items[i];
            if(item.Id==id)
            {
                item.Num += num;
                find = true;
                break;
            }
        }
        if(!find)
            bagData.items.Add(new Item(id,num));
        if(autoSave)
        {
            Save();
        }
        MarkDirty();
    }

    public void DeleteItem(int id,int num, bool autoSave = false)
    {
        for(int i=0;i<bagData.items.Count;i++)
        {
            var item = bagData.items[i];
            if (item.Id == id)
            {
                item.Num -= num;
                if (item.Num <= 0)
                {
                    bagData.items.Remove(item);
                }
                break;
            }
        }
        if (autoSave)
        {
            Save();
        }
        MarkDirty();
    }
    
    //[ContextMenu("add item")]
    //public void AddItem()
    //{
    //    Debug.Log("add item");
    //}
}

[System.Serializable]
public class BagData:BaseData
{
    public List<Item> items = new List<Item>();
}