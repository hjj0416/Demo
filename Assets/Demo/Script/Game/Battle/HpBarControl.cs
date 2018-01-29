using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarControl : MonoSingleton<HpBarControl>
{

    [SerializeField] GameObject prefab;
    

    public HealthBar CreateHpBar(Transform role)
    {
        GameObject hp = Instantiate(prefab);
        hp.SetActive(true);
        Transform tf = hp.transform;
        tf.SetParent(gameObject.transform);
        tf.localScale = Vector3.one;
        HealthBar bar = hp.GetComponent<HealthBar>();
        bar.target = role;
        return bar;
    }

    public Vector3 WorldToUIPos(Vector3 worldPos,float height)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        screenPos.z = 0;
        return new Vector3(screenPos.x - Screen.width / 2 + 0, screenPos.y - Screen.height / 2 + height, screenPos.z);

    }
}
