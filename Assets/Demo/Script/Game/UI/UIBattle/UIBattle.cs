using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattle : UIWin
{

    protected override void OnOpened()
    {
        base.OnOpened();
        UIManager.AddEventHandler<HeroController>(UIEventType.ON_CANCEL_REBORN,OnExitBattle);

    }

    protected override void OnClosed()
    {
        base.OnClosed();
        UIManager.RemoveEventHandler<HeroController>(UIEventType.ON_CANCEL_REBORN,OnExitBattle);
    }

    void OnExitBattle(short id, HeroController hero)
    {
        SceneMgr.Instance.EnterScene("Lobby");
        UIManager.Instance.CloseWindow("UISetting");
        UIManager.Instance.CloseWindow("UIBattle");
        UIManager.Instance.ShowWindow("UILobby");
        CloseSelf();
    }

}
