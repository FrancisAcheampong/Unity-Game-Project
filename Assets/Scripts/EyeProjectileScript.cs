using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeProjectileScript : MonoBehaviour {

	private Rigidbody2D rb;
	public Vector2 direction = Vector2.right;
	public float projectileSpeed;
	private int animState;
	public int animFreq;
	private int currentAnim = 0;
	public Sprite[] sprites = new Sprite[3];

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.transform.name.Contains("Sol")){
			GameObject.Destroy(this.gameObject, 0.01f);
		} else if (coll.transform.name.Contains("Player")){
			coll.gameObject.GetComponent<PlayerHealth>().Hurt(3);
		}
	}

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		transform.position += new Vector3(0, 0, 1);
	}

	void Update(){
		rb.velocity = direction * projectileSpeed;
		animState++;
		if (animState > animFreq){
			animState = 0;
			switch(currentAnim){
				case 0:
					currentAnim = 1;
					break;
				case 1:
					currentAnim = 2;
					break;
				case 2:
					currentAnim = 0;
					break;
			}
		}
		GetComponent<SpriteRenderer>().sprite = sprites[currentAnim];
	}

	public void Setup(){
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
 		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
