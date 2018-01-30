using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour {

    [SerializeField] Slider HPSlider;
    [SerializeField] Text HPText;

    [SerializeField] Slider ExpSlider;
    [SerializeField] Text EXPText;

    [SerializeField] Text LvText;

    // Use this for initialization

    private void Awake()
    {
        PlayerDataMgr.AddEventHandler<HeroController>(PlayerDataEvent.PLAYER_HP_CHANGE, HPChange);
        PlayerDataMgr.AddEventHandler<PlayerData>(PlayerDataEvent.EXP_CHANGED, EXPChange);
        PlayerDataMgr.AddEventHandler<PlayerData>(PlayerDataEvent.LEVEL_CHANGED, LevelChange);
        Init();
    }

    private void OnDestroy()
    {
        PlayerDataMgr.RemoveEventHandler<HeroController>(PlayerDataEvent.PLAYER_HP_CHANGE, HPChange);
        PlayerDataMgr.RemoveEventHandler<PlayerData>(PlayerDataEvent.EXP_CHANGED, EXPChange);
        PlayerDataMgr.RemoveEventHandler<PlayerData>(PlayerDataEvent.LEVEL_CHANGED, LevelChange);
    }

    void Init()
    {
        PlayerData playerData = PlayerDataMgr.Instance.playerData;
        EXPChange(-1,playerData);
        LevelChange(-1,playerData);
    }

    void EXPChange(short evt, PlayerData playerData)
    {
        ExpSlider.value = (float)playerData.Exp / playerData.LevelUpExp;
        EXPText.text = string.Format("EXP:{0}", playerData.Exp);
    }

    void LevelChange(short evt, PlayerData playerData)
    {
        LvText.text = string.Format("LV:{0}", playerData.Level);
        if (evt>=0&&playerData.Level>1)
            UIManager.Instance.ShowTips(string.Format("等级提升至{0}级", playerData.Level));
    }

    void HPChange(short id, HeroController hero)
    {
        HPSlider.value = (float)hero.playerStatus.status.hp / hero.playerStatus.hpMax;
        HPText.text = string.Format("{0}/{1}", hero.playerStatus.status.hp, hero.playerStatus.hpMax);
    }
}
