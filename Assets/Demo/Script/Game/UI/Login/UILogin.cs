using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : UIWin {
    [SerializeField] Button LoginButton;
    [SerializeField] InputField AccountInput;
    [SerializeField] InputField PasswordInput;

    protected override void OnAddUIEvent()
    {
        base.OnAddUIEvent();
        LoginButton.onClick.AddListener(OnClick);
    }

    protected override void OnRemoveUIEvent()
    {
        base.OnRemoveUIEvent();
        LoginButton.onClick.RemoveListener(OnClick);
    }

    void OnClick()
    {
        Debug.Log(AccountInput.text);
        Debug.Log(PasswordInput.text);

        SceneMgr.Instance.EnterScene("Lobby", () =>
        {
            UIManager.Instance.ShowWindow("UILobby");

            CloseSelf();
        });
    }

}
