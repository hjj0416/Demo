using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInfo : MonoBehaviour {

    [SerializeField] Button IconBtn;
    [SerializeField] Text LevelTxt;
    [SerializeField] Text NameTxt;
    [SerializeField] Slider HpSlider;
    [SerializeField] Text HpText;

    public void SetData( PlayerData data)
    {
        LevelTxt.text = string.Format("Lv.{0}",data.Level);
    }

}
