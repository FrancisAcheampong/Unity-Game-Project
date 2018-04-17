using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private BoxCollider2D boxCollider;
	private Rigidbody2D rb;

	public int playerNo = 1;

	[Header("Movement")]
	[Range(1,20)]
	public float movementSpeed;

	[Space(10)]
	
	[Header("Dodge and Recover")]

	public float dodgeForce;
	[Range(-0.1f,3f)]
	public float distanceToRecover;
	public float dodgeDrag;
	
	[Space(10)]

	private float defaultDrag;
	private Vector2 defaultBoxSize;
	private bool isDodging = false;

	[Space(10)]

	public Sprite normalSprite;
	public Sprite dodgeSprite;

	public float maxHealth;
	private float health;

	private Transform SlashAttack;
	private Transform SpearAttack;
	public float slashDistance;
	private bool canMove = true;


	[Space(10)]
	[Header("Items")]

	public int bombs;
	public int keys;
	public GameObject arrowObj;

	private bool canShoot = true;

	private enum xdir{
		none,
		left,
		right
	}
	private xdir xDirection = xdir.none;
	private enum ydir{
		none, 
		up, 
		down
	}
	private ydir yDirection = ydir.up;

	[HideInInspector]
	public enum wpn{
		none,
		sword,
		bow,
		spear

	}
	[HideInInspector]
	public wpn equippedWeapon = wpn.spear;
	
	private Vector2 direction;


	void Start () {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();

		defaultDrag = rb.drag;

		health = maxHealth;
		SlashAttack = transform.Find("SlashArea");
		SlashAttack.gameObject.SetActive(false);
		
		SpearAttack = transform.Find("SpearArea");
		SpearAttack.gameObject.SetActive(false);

		//temporal items
		bombs = 0;
		keys  = 0;
		equippedWeapon = wpn.sword;
		UpdateHUD();
	}

	static int Sign(float number) {
      return number < 0 ? -1 : (number > 0 ? 1 : 0);
  	}
	
	void Update () {
		
		//Movement
		if(isDodging && Vector2.Distance(rb.velocity, Vector2.zero) < distanceToRecover ){
			isDodging = false; //return isDodging boolean to false
			rb.drag = defaultDrag; //restore normal linear Drag
			boxCollider.size = defaultBoxSize; // TODO: program proper dodging mechanics
			transform.localScale = Vector2.one; // return transform scale to 1,1

			spriteRenderer.sprite = normalSprite;

		} else if (isDodging){
			//transform.localScale = new Vector2(2, 2);
			spriteRenderer.sprite = dodgeSprite;
		}

		if (!isDodging){

			rb.velocity = Vector2.zero;

			if (canMove){
				if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)){
					rb.velocity += Vector2.left * movementSpeed;
					xDirection = xdir.left;
					direction = Vector2.left;
				} else if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D)){
					rb.velocity += Vector2.right * movementSpeed;
					xDirection = xdir.right;
					direction = Vector2.right;
				} else {
					yDirection = ydir.none;
				}

				if(Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W)){
					rb.velocity += Vector2.up * movementSpeed;
					yDirection = ydir.up;
					direction = Vector2.up;
				} else if (Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S)){
					rb.velocity += Vector2.down * movementSpeed;
					yDirection = ydir.down;
					direction = Vector2.down;
				} else {
					yDirection = ydir.none;
				}

				if (Input.GetKeyDown(KeyCode.Space)){
					rb.velocity = new Vector2( Sign( rb.velocity.x ), Sign( rb.velocity.y ) ) * dodgeForce;
					rb.drag = dodgeDrag;
					defaultBoxSize = boxCollider.size;
					//boxCollider.size = Vector2.zero;
					isDodging = true;

				}
			}

			if (Input.GetKeyDown(KeyCode.J)||Input.GetMouseButtonDown(0)){
				if (equippedWeapon == wpn.sword || equippedWeapon == wpn.spear){
					AttackSlash();
				} else if (equippedWeapon == wpn.bow && canShoot){
					AttackBow();
				}
			}

			if (Input.GetKeyDown(KeyCode.Alpha1)){
				equippedWeapon = wpn.sword;
				UpdateHUD();
			} else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetMouseButtonDown(1))
            {
				equippedWeapon = wpn.bow;
				UpdateHUD();
			} else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetMouseButtonDown(2))
            {
				equippedWeapon = wpn.spear;
				UpdateHUD();
			}
		}

	}

	void AttackBow(){
		var obj = Instantiate(arrowObj, this.transform.position + new Vector3(0, 0, -2f), Quaternion.identity);
		obj.GetComponent<ArrowBehaviour>().direction = direction;
		obj.GetComponent<ArrowBehaviour>().Setup();
		StartCoroutine(wait(0.15f));
		StartCoroutine(shootLapse());
	}

	void AttackSlash(){
		var atk = SlashAttack;
		if (equippedWeapon == wpn.sword){
			atk = SlashAttack;
			atk.gameObject.SetActive(true);
			atk.localPosition = Vector2.zero;
			float atkDis = slashDistance;
			atk.transform.localPosition = (direction.normalized*atkDis);
			StartCoroutine(hideAtack(atk.gameObject,0.23f));

		} else if (equippedWeapon == wpn.spear){
			atk = SpearAttack;
			atk.gameObject.SetActive(true);
			atk.localPosition = Vector2.zero;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
 			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			StartCoroutine(hideAtack(atk.gameObject,0.15f));
		}
		
		
	}

	private IEnumerator hideAtack(GameObject atk, float duration = 0.3f){
		canMove = false;
		yield return new WaitForSeconds(duration);
		atk.SetActive(false);
		canMove = true;
	}

	private IEnumerator wait(float lenght = 0.3f){
		canMove = false;
		yield return new WaitForSeconds(lenght);
		canMove = true;
	}

	private IEnumerator shootLapse(float lenght = 0.4f){
		canShoot = false;
		yield return new WaitForSeconds(lenght);
		canShoot = true;
	}

	public void UpdateHUD(){
		GameObject.Find("Player Panel").GetComponent<UIPlayerHUD>().UpdateHud();
	}

	public void Hurt(float dmg){
		// health -= dmg;
		// if (health < 0) health = 0;
		// print("Health is:" + health);

		var hp = GetComponent<PlayerHealth>();
		hp.Hurt(dmg);
		

	}

	public void KnockBack(float force, Vector2 enemyPos){
		rb.velocity = Vector2.zero;
		rb.AddForce( (transform.position - (Vector3)enemyPos).normalized * force );
		//StartCoroutine(wait(0.3f));
	}
}
