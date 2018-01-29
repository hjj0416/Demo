using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBag : UIWin {

    [SerializeField] UIGrid UIGrid;
    [SerializeField] GameObject UIItem;

    //public Dictionary<int, Item> ItemList;

    protected override void OnOpened()
    {
        //BagDataManager.Instance.AddItem(10001,10);
        //BagDataManager.Instance.AddItem(10002, 5);
        //BagDataManager.Instance.OnSaveItems();
        base.OnOpened();
        BagDataManager.Instance.dispatcher.AddEventHandler(BagDataEvent.DATA_CHANGED, BagUpdate);
        GetItem();
    }

    protected override void OnClosed()
    {
        BagDataManager.Instance.dispatcher.RemoveEventHandler(BagDataEvent.DATA_CHANGED, BagUpdate);
        base.OnClosed();
    }

    void Load()
    {
        Dictionary<int, BagItemBean> beans = BagItemBean.beans;
    }

    public void GetItem()
    {
        Dictionary<int, BagItemBean> beans = BagItemBean.beans;
        List<Item> item=BagDataManager.Instance.bagData.items;
        foreach(var v in item)
        {
            Transform emptyGrid = UIGrid.GetEmptyGrid();
            if (emptyGrid == null)
            {
                Debug.LogWarning("背包已满!!");
                return;
            }
            BagItemBean temp;
            if(beans.TryGetValue(v.Id, out temp))
                this.InitItem(temp, emptyGrid,v.Num,v.Id);
        }
    }

    public void InitItem(BagItemBean item, Transform parent,int num,int id)
    {
        GameObject itemPrefab = Instantiate(UIItem);
        itemPrefab.transform.SetParent(parent);
        if (!itemPrefab.activeSelf)
            itemPrefab.SetActive(true);
        itemPrefab.transform.localPosition = Vector3.zero;
        itemPrefab.transform.localScale = Vector3.one;
        UIItem uiItem = itemPrefab.GetComponent<UIItem>();
        uiItem.ItemNum = num;
        uiItem.ItemId = id;
        uiItem.UpdateItem(item.icon, item);
        uiItem.UpdateNum();

    }

    public void BagUpdate(short evt)
    {
        for (int i = 0; i < UIGrid.Grids.Length; i++)
        {
            var tf = UIGrid.Grids[i];
            if (tf.childCount>0)
                DestroyImmediate(tf.GetChild(0).gameObject);
        }
        GetItem();
    }
}
