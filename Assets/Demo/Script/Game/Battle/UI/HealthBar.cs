using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField] Slider HpBar;
    [SerializeField] float HideTime;
    public float Height;

    //血条的角色
    public Transform target;
    EnemyController enemyCtrl;
    private RectTransform hpTransform;
    
	void Start () {
        hpTransform = transform as RectTransform;
        HpBar.value = 1;
        enemyCtrl = target.GetComponent<EnemyController>();
        enemyCtrl.dispatcher.AddEventHandler(RoleEvent.GOT_DAMAGE,OnGotDamage);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        enemyCtrl.dispatcher.RemoveEventHandler(RoleEvent.GOT_DAMAGE, OnGotDamage);
    }

    void Update () {
        hpTransform.anchoredPosition = HpBarControl.Instance.WorldToUIPos(target.position,Height);
        if(HpBar.value==1)
            gameObject.SetActive(false);
    }

    public void SetHP(int hp,float maxHP)
    {
        HpBar.value = (float)hp / maxHP;
    }

    private void OnGotDamage(short evtId)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }
}
