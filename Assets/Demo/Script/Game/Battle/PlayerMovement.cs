using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;

    private HeroController _heroCtrl;
    private Camera _camera;
    private CharacterController _controller;

	void Start () {
        _heroCtrl = GetComponent<HeroController>();
        _controller = GetComponent<CharacterController>();
        _camera = Camera.main;
        UIManager.AddEventHandler(UIEventType.ON_JOYSTICK_MOVE_START, OnMoveStart);
        UIManager.AddEventHandler<Vector2>(UIEventType.ON_JOYSTICK_MOVE, OnMove);
        UIManager.AddEventHandler(UIEventType.ON_JOYSTICK_MOVE_END, OnMoveEnd);
        UIManager.AddEventHandler(UIEventType.ON_JOYSTICK_ATTACK, OnAttack);
    }

    private void OnDestroy()
    {
        UIManager.RemoveEventHandler(UIEventType.ON_JOYSTICK_MOVE_START, OnMoveStart);
        UIManager.RemoveEventHandler<Vector2>(UIEventType.ON_JOYSTICK_MOVE, OnMove);
        UIManager.RemoveEventHandler(UIEventType.ON_JOYSTICK_MOVE_END, OnMoveEnd);
        UIManager.RemoveEventHandler(UIEventType.ON_JOYSTICK_ATTACK, OnAttack);
    }
    
    void Update () {
        var dir = _camera.transform.rotation.eulerAngles;
        dir.x = 0;
        _controller.transform.rotation = Quaternion.Euler(dir);
	}

    void OnMoveStart(short evtId)
    {
        _heroCtrl.ctrlAnimState = HeroController.ControlAnimationState.Move;
    }

    void OnMove(short evtId, Vector2 axis)
    {
        Vector3 mov_pos = new Vector3(axis.x, 0, axis.y * 0.5f) * speed * Time.deltaTime;
        mov_pos = transform.TransformDirection(mov_pos);
        mov_pos = new Vector3(mov_pos.x, 0, mov_pos.z);
        _controller.SimpleMove(mov_pos);

    }

    void OnMoveEnd(short evtId)
    {
        _heroCtrl.ctrlAnimState = HeroController.ControlAnimationState.Idle;
    }

    void OnAttack(short evtId)
    {
        
    }
}
