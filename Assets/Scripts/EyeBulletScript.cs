using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBulletScript : MonoBehaviour {

	private Rigidbody2D rb;
	public Vector2 direction = Vector2.right;
	public float projectileSpeed;

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.transform.name.Contains("Sol")){
			GameObject.Destroy(this.gameObject, 0.01f);
		}
	}

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		//transform.position += new Vector3(0, 0, 1);
	}

	void Update(){
		rb.velocity = direction * projectileSpeed;
	}

	public void Setup(){
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
 		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
