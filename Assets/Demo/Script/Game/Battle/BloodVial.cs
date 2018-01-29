using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodVial : MonoBehaviour {

    [SerializeField] int max;
    [SerializeField] int min;
    [SerializeField] float destroyTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0)
            Destroy(gameObject);
	}

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            //this.GetComponent<Rigidbody>().isKinematic = true;
            //var player=target.gameObject.GetComponent<HeroController>();
            //player.GetHeal(Random.Range(min,max));
            BagDataManager.Instance.AddItem(10001,1, true);
            Destroy(gameObject);
            UIManager.Instance.ShowTips("获得血药*1");
        }
    }
}
