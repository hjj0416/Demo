using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackBtn : MonoBehaviour 
{
    public void OnAttack()
    {
        UIManager.SendEvent(UIEventType.ON_JOYSTICK_ATTACK);
    }
}
