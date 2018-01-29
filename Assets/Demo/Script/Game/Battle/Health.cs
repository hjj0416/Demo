using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField] float height;
    public HealthBar healthBar;


    void Start () {
        healthBar= HpBarControl.Instance.CreateHpBar(gameObject.transform);
        healthBar.Height = height;
	}

    public void GetDamage(int damage,float maxHP)
    {
        healthBar.SetHP(damage,maxHP);
    }
	
}
