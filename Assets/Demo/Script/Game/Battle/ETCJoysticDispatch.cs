using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETCJoysticDispatch : MonoBehaviour {

    [SerializeField]ETCJoystick joystick;

	void Start () {

        joystick.onMoveStart.AddListener(OnStartMove);
        joystick.onMove.AddListener(OnMove);
        joystick.onMoveEnd.AddListener(OnMoveEnd);
    }
	
    void OnStartMove()
    {
        UIManager.SendEvent(UIEventType.ON_JOYSTICK_MOVE_START);
    }

    void OnMove(Vector2 axis)
    {
        UIManager.SendEvent<Vector2>(UIEventType.ON_JOYSTICK_MOVE, axis);
    }

    void OnMoveEnd()
    {
        UIManager.SendEvent(UIEventType.ON_JOYSTICK_MOVE_END);
    }
}
