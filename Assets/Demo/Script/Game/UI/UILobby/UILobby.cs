using UnityEngine;
using UnityEngine.UI;

public class UILobby : UIWin
{
    [SerializeField] UIPlayerInfo PlayerInfo;
    [SerializeField] private Button BattleBtn;

    protected override void OnAddUIEvent()
    {
        base.OnAddUIEvent();
        BattleBtn.onClick.AddListener(OnClickBattle);
    }

    protected override void OnRemoveUIEvent()
    {
        base.OnRemoveUIEvent();
        BattleBtn.onClick.RemoveListener(OnClickBattle);
    }

    protected override void OnOpened()
    {
        base.OnOpened();
        
    }


    private void OnClickBattle()
    {
        if (PlayerDataMgr.Instance.playerData.Energy >= 10)
            UIManager.Instance.Alert("前往战斗","勇士，即将前往战斗场景，您是否已经准备完毕？ (消耗10点体力)", AlertMode.OkCancel, OnAlert);
        else
            UIManager.Instance.Alert("体力不足！请购买体力",AlertMode.Ok);
    }

    private void OnAlert(AlertResult result)
    {
        if(result == AlertResult.OK)
        {
            PlayerDataMgr.Instance.playerData.StartBattle();
            CloseSelf();
            SceneMgr.Instance.EnterScene("Battle1", ()=> 
            {
                UIManager.Instance.ShowWindow("UIBattle");
            });
        }
    }
}
