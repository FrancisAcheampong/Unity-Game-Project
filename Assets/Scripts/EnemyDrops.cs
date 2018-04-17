using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour {

	public GameObject loot;
	public int chance;
	public int rolls;
	public int amount;
	public float range;

	void Start(){
	}

	public void Drop(Vector2 dir){
		var i = 0;
		while(i++ < rolls){
			if (Random.Range(1, 100) > 100-chance){
				while(i++ < amount){
					var o = Instantiate(loot, this.transform.position, Quaternion.identity);
					if (dir == Vector2.zero)
						o.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-range,range), Random.Range(-range,range));
					else 
						o.GetComponent<Rigidbody2D>().velocity = (((Vector2)this.transform.position - dir) + new Vector2(Random.Range(-range,range), Random.Range(-range,range))).normalized * range;
				}
			}
		}
	}

	public void Drop(){
		var i = 0;
		while(i++ < rolls){
			if (Random.Range(1, 100) > 100-chance){
				while(i++ < amount){
					var o = Instantiate(loot, this.transform.position, Quaternion.identity);
					o.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-range,range), Random.Range(-range,range));
				}
			}
		}
	}
}
