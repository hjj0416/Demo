/// <summary>
/// Player status.
/// This script use to adjust a status hero
/// </summary>

using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	
	public string playerName; //Hero name

    public int hpMax, mpMax;

    [System.Serializable]
	public class Attribute
	{
		public int lv,hp,mp,atk,def,spd,hit;
		public float criticalRate,atkSpd,atkRange,movespd,exp;
	}
	
	
	public Attribute status;  //main status
	
	public void Init()
    {
        status.hp = hpMax;
        status.mp = mpMax;

    }
    
}
