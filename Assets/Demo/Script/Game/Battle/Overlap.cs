using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlap
{

    public static void OverlapSphere(HeroController heroCtrl, Vector3 pos, float range, int maskTarget)
    {
        //返回相交球的所有碰撞体
        Collider[] cols = Physics.OverlapSphere(pos, range, maskTarget);
        if (cols.Length > 0)
        {
            int len = cols.Length;
            for (int i = 0; i < len;i++)
            {
                Collider currentCollider = cols[i];
                var targetTemp = currentCollider.gameObject.GetComponent<EnemyController>();
                if (targetTemp != null && targetTemp.enemyStatus.status.hp > 0)
                {
                    heroCtrl.target = targetTemp.gameObject;
                    Debug.Log("find enemy "+ targetTemp.gameObject.name);
                }
            }

        }
    }
}
