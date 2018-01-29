using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UIWin {
    [SerializeField] Toggle BgmToggle;
    [SerializeField] Slider BgmSlider;
    [SerializeField] Button ExitButton;

    protected override void OnOpened()
    {
        base.OnOpened();
        int value=PlayerPrefs.GetInt("BGMToggle",1);
        bool isOn = false;
        if (value == 1)
            isOn = true;
        BgmToggle.isOn = isOn;

       BgmSlider.value=PlayerPrefs.GetFloat("BGMVoice",1);
    }

    protected override void OnAddUIEvent()
    {
        base.OnAddUIEvent();
        BgmToggle.onValueChanged.AddListener(OnBGMValueChanged);
        BgmSlider.onValueChanged.AddListener(OnBGMVoiceChanged);
        ExitButton.onClick.AddListener(OnExitClick);
    }

    void OnBGMValueChanged(bool value)
    {
        PlayerPrefs.SetInt("BGMToggle",value?1:0);
        //Camera.main.GetComponent<AudioSource>().enabled = value;
        GameObject.Find("SoundManager").GetComponent<AudioSource>().enabled = value;
    }

    void OnBGMVoiceChanged(float value)
    {
        PlayerPrefs.SetFloat("BGMVoice",value);
        //Camera.main.GetComponent<AudioSource>().volume = value;
        GameObject.Find("SoundManager").GetComponent<AudioSource>().volume = value;
    }

    void OnExitClick()
    {
        if (SceneMgr.Instance.curSceneName == "Battle1")
        {
            SceneMgr.Instance.EnterScene("Lobby");
            UIManager.Instance.CloseWindow("UISetting");
            UIManager.Instance.CloseWindow("UIBattle");
            UIManager.Instance.ShowWindow("UILobby");
            CloseSelf();
        }
        else if (SceneMgr.Instance.curSceneName == "Lobby")
        {   
            SceneMgr.Instance.EnterScene("Start");
            UIManager.Instance.CloseWindow("UILobby");
            UIManager.Instance.ShowWindow("UILogin");
            CloseSelf();
        }
    }

}
