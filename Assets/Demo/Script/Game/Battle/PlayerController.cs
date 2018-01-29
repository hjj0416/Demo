using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] Animation animation;

	public void OnStartMove()
    {
        Debug.Log("start move");
        animation.Play("Run");
    }

    public void OnEndMove()
    {
        Debug.Log("end move");
        animation.Play("Idle");
    }
}
