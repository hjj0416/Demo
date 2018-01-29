using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform _transform;
    [SerializeField] float speed = 10;

	void Start () {
        UIManager.AddEventHandler(UIEventType.ON_JOYSTICK_MOVE_START, OnMoveStart);
        UIManager.AddEventHandler<Vector2>(UIEventType.ON_JOYSTICK_MOVE, OnMove);
        UIManager.AddEventHandler(UIEventType.ON_JOYSTICK_MOVE_END, OnMoveEnd);

        _transform = transform;
    }

    private void OnDestroy()
    {
        UIManager.RemoveEventHandler(UIEventType.ON_JOYSTICK_MOVE_START, OnMoveStart);
        UIManager.RemoveEventHandler<Vector2>(UIEventType.ON_JOYSTICK_MOVE, OnMove);
        UIManager.RemoveEventHandler(UIEventType.ON_JOYSTICK_MOVE_END, OnMoveEnd);
    }
    
    void Update () {
		
	}

    void OnMoveStart(short evtId)
    {

    }

    void OnMove(short evtId, Vector2 axis)
    {
        Debug.Log(axis);
        _transform.Translate(Vector3.forward * axis.y*Time.deltaTime*speed);
    }

    void OnMoveEnd(short evtId)
    {

    }
}
