using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossManagerScript : MonoBehaviour {

	public enum st{
		sleep,
		idle
	}
	public st state;

	public enum stage{
		none,
		easy,
		medium,
		hard,
		done
	}
	public stage currentStage;

	private FollowPlayerPupilScript pupil;
	public bool vulnerable = false;

	public GameObject blockDoor;

	private char heart = '*'; //\u2665

	private int attackCounter = 0;
	private int attackFrequency = 0;

	[Header("Objects")]
	public GameObject wave1,wave2,wave3;
	public GameObject layer2, layer3;

	public GameObject bullet;

	void OnCollisionEnter2D(Collision2D coll){
		//print(coll.gameObject.name);
		if (coll.gameObject.name.Contains("player") && currentStage == stage.none){
			StartCoroutine(AwakeEye());
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (vulnerable){
			if (coll.gameObject.name.Contains("Area") || coll.gameObject.name.Contains("Arrow")){
				if (currentStage == stage.easy){
					setupStage(stage.medium);
					vulnerable = false;

				} else if (currentStage == stage.medium){
					setupStage(stage.hard);
					vulnerable = false;

				} else if (currentStage == stage.hard){
					setupStage(stage.done);
				}
			}
		}
	}

	void Start () {
		currentStage = stage.none;	
		state = st.sleep;

		pupil = GameObject.Find("Pupil").GetComponent<FollowPlayerPupilScript>();
		pupil.followPlayer = false;
		pupil.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

	}
	
	void Update () {

		if (state == st.idle && !vulnerable){
			if (attackCounter < attackFrequency){
				attackCounter++;
			} else {
				if (currentStage != stage.done){
					Shoot();

				}
			}
		}
	}

	private void Shoot(){
		attackCounter = 0;
		var o = Instantiate(bullet, pupil.transform.position + new Vector3(0, 0, -0.1f), Quaternion.identity);
		o.GetComponent<EyeBulletScript>().direction = (GameObject.Find("player1").transform.position - transform.position).normalized;

	}
	
	private IEnumerator AwakeEye(){
		blockDoor.SetActive(true);
		while(pupil.GetComponent<SpriteRenderer>().color.a < 1){
			pupil.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.04f);
			yield return new WaitForSeconds(0.1f);
		}
		flashScreen();
		GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisplayMessage(true, false);
		GameObject.Find("GameManager").GetComponent<GameManagerScript>().messageDisplay.text = "Boss\n ♥ ♥ ♥";
		pupil.GetComponent<SpriteRenderer>().color = Color.white;
		state = st.idle;
		setupStage(stage.easy);
		pupil.followPlayer = true;
		GetComponent<AudioSource>().Play();
	}

	private void setupStage(stage stage){
		vulnerable = false;
		if (stage == stage.easy){
			currentStage = stage.easy;
			GetComponent<SpriteRenderer>().color = Color.white;
			attackFrequency = 100;
			pupil.followPlayer = true;
			wave1.SetActive(true);


		} else if (stage == stage.medium){
			flashScreen();
			currentStage = stage.medium;
			layer2.SetActive(true);
			GetComponent<SpriteRenderer>().color = new Color(1, 0.7f, 0.7f, 1);
			attackFrequency = 50;
			wave2.SetActive(true);
			GameObject.Find("GameManager").GetComponent<GameManagerScript>().messageDisplay.text = "Boss\n" + heart.ToString() + heart.ToString() + " - ";

		} else if (stage == stage.hard){
			flashScreen();
			currentStage = stage.hard;
			layer2.SetActive(true);
			GetComponent<SpriteRenderer>().color = new Color(1, 0.4f, 0.4f, 1);
			attackFrequency = 20;
			wave3.SetActive(true);
			GameObject.Find("GameManager").GetComponent<GameManagerScript>().messageDisplay.text = "Boss\n" + heart.ToString() + " - - ";
		} else if (stage == stage.done){
			GameObject.Find("GameManager").GetComponent<GameManagerScript>().messageDisplay.text = "Boss\n - - - ";
			StartCoroutine(EndGame());
		}
	}

	private void flashScreen(){
		GameObject.Find("FlashPanel").GetComponent<Image>().color = new Color(1, 1, 1, 1);
		StartCoroutine(RemoveFlash());
	}

	private IEnumerator EndGame(){
		flashScreen();
		yield return new WaitForSeconds(1);
		flashScreen();
		yield return new WaitForSeconds(0.7f);
		flashScreen();
		yield return new WaitForSeconds(0.4f);
		flashScreen();
		yield return new WaitForSeconds(0.1f);
		flashScreen();
		yield return new WaitForSeconds(0.1f);
		flashScreen();

		pupil.vibRange = 1;
		pupil.GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(4f);
		SceneManager.LoadScene(5);
		//GetComponent<AudioSource>().Stop();
	}

	private IEnumerator RemoveFlash(){
		while(GameObject.Find("FlashPanel").GetComponent<Image>().color.a > 0){
			GameObject.Find("FlashPanel").GetComponent<Image>().color -= new Color(0, 0, 0, 0.04f);
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void WaveDone(int wave){
		switch(wave){
			case 1:
				vulnerable = true;
				pupil.vibRange = 0.05f;
				
				//setupStage(stage.medium);
				print("wave 1 done");
				break;

			case 2:
				vulnerable = true;
				pupil.vibRange = 0.15f;

				//setupStage(stage.hard);
				print("wave 2 done");
				break;

			case 3:
				vulnerable = true;
				pupil.vibRange = 0.24f;

				//setupStage(stage.done);
				print("game beaten");
				break;
		}
	}
}
