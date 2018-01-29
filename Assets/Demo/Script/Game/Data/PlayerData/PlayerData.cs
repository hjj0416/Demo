using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData :BaseData{
    public string Name;
    public int Level;
    public int Exp;
    public int Coin;
    public int Gem;
    public int Energy;
    public float EnergySlider;
    public int LevelUpExp;

    public void AddCoin(int count)
    {
        Coin += count;
        Save();
        PlayerDataMgr.Instance.dispatcher.SendEvent<int>(PlayerDataEvent.COIN_CHANGED,Coin);
    }

    public void AddGem(int count)
    {
        Gem += count;
        Save();
        PlayerDataMgr.Instance.dispatcher.SendEvent<int>(PlayerDataEvent.GEM_CHANGED,Gem);
    }

    public void AddEnergy(int energyCount,int coinCount)
    {
            Energy += energyCount;
        //if (Energy >= 150)
        //    Energy = 150;
        Coin -= coinCount;
        Save();
        PlayerDataMgr.Instance.dispatcher.SendEvent<int>(PlayerDataEvent.ENERGY_CHANGED,Energy);
        PlayerDataMgr.Instance.dispatcher.SendEvent<int>(PlayerDataEvent.COIN_CHANGED, Coin);
        Debug.Log(string.Format("购买体力：{0},消耗金币：{1}",energyCount,coinCount));
    }

    public void AddExp(int count)
    {
        Exp += count;
        while (Exp >= LevelUpExp)
        {
            Exp -= LevelUpExp;
            AddLevel();
        }
        Save();
        PlayerDataMgr.Instance.dispatcher.SendEvent<PlayerData>(PlayerDataEvent.EXP_CHANGED,this);
        
    }

    public void AddLevel()
    {
        Level++;
        LevelUpExp = (int)Mathf.Pow(Level, 2) * 10 + 100;
        Save();
        PlayerDataMgr.Instance.dispatcher.SendEvent<PlayerData>(PlayerDataEvent.LEVEL_CHANGED, this);
    }

    public void StartBattle()
    {
        Energy -= 10;
        EnergySlider = (float)Energy / 150;
        Save();
        PlayerDataMgr.Instance.dispatcher.SendEvent<int>(PlayerDataEvent.ENERGY_CHANGED,Energy);
        Debug.Log("进入战斗场景消耗体力："+10);
    }

    private void Save()
    {
        PlayerDataMgr.Instance.Save();
    }
}
