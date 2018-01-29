/// <summary>
/// Drop item.
/// This script use to control a enemy drop item
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropItem : MonoBehaviour {

    [SerializeField] float powerImpulse = 1;
    [SerializeField] int dropRate = 50;
	public List<GameObject> item_Drop_List = new List<GameObject>();
	
	public void TryDropItem()
    {
		for(int i = 0; i < item_Drop_List.Count; i++)
        {
            int randomNum = Random.Range(0, 100);
            if (randomNum < dropRate)
                continue;

            GameObject go = (GameObject)Instantiate(item_Drop_List[i], transform.position + (Vector3.up), Quaternion.identity);
            go.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1) * 1.5f * powerImpulse, 1 * 3 * powerImpulse, Random.Range(-1, 1) * 1.5f * powerImpulse), ForceMode.Impulse);
        }
	}
	
	
}
