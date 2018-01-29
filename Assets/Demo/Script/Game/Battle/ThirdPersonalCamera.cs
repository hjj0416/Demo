using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonalCamera : MonoBehaviour {

    [SerializeField] bool rotate = false;
    [SerializeField] float angle = 10;

    /// <summary>
    /// player的Transform
    /// </summary>
    private Transform player;

    /// <summary>
    /// 摄像机与player之间的方向向量
    /// </summary>
    private Vector3 direction;


    void Start()
    {
        UIManager.AddEventHandler<Vector2>(UIEventType.ON_JOYSTICK_MOVE, OnMove);
    }

    private void OnDestroy()
    {
        UIManager.RemoveEventHandler<Vector2>(UIEventType.ON_JOYSTICK_MOVE, OnMove);
    }


    void OnMove(short evtId, Vector2 axis)
    {
        Camera.main.transform.RotateAround(Vector3.zero, Vector3.up*axis.x, angle*Time.deltaTime);
    }


    void Update()
    {
        if(player==null)
        {
            //获取到player的transform(Tags.Player是player的标签，如果有不明白什么意思的，可以看我前几篇文章，标签的管理)
            player = GameObject.FindWithTag("Player").transform;
            if(player!=null)
            {
                //计算player到camera的方向向量的距离
                direction = player.position - transform.position;
            }
            else
            {
                return;
            }
        }
        else
        {
            //移动摄像机，使摄像机与player保持一定的方向向量
            //camera当前的位置=player的位置减去方向向量
            transform.position = player.position - direction;
        }
    }
}
