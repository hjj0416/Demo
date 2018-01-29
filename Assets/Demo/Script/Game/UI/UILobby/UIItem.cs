using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIItem : MonoBehaviour
{
    [SerializeField] Text NumText;
    [SerializeField] Image IconImage;
    public int ItemId;
    public int ItemNum;
    public BagItemBean item;
    [SerializeField] GameObject Menu;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void UpdateItem(string icon,BagItemBean bagItemBean)
    {
        item = bagItemBean;
        IconImage.sprite = Resources.Load<Sprite>(icon);
    }

    public void UpdateNum()
    {
        if (ItemNum == 0)
        {
            Destroy(gameObject);
        }
        else    
            NumText.text = ItemNum.ToString();
    }

    void OnClick()
    {
        BagDataManager.SendEvent<UIItem>(BagDataEvent.CLICK_ITEM,this);
        //if (item.type == 1)
        //{
        //    EquipmentDataMgr.Instance.Dress(item);
        //    EquipmentDataMgr.Instance.dispatcher.SendEvent<BagItemBean>(EquimentDataEvent.EQUUIMENT_CHANGED, item);

        //}

        //if (item.ID == 10001)
        //{
        //    var player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroController>();
        //    player.GetHeal(100);
        //}

        //PlayerDataMgr.Instance.Save();
        //BagDataManager.Instance.DeleteItem(ItemId, 1, true);

    }
}
