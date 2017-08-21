using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {

	// Use this for initialization

	Rigidbody rb;
	float speed = 7;
	public Text txtScore;
	public Text myScore;
	public Text bestScore;
	int score;


	public ParticleSystem pSystem;
	public ParticleSystem pSystemEnemy;
	public GameObject panel;
	public GameObject panel2;
	int HighScore;

	//ArrayList list = new ArrayList ();

	bool isGameOver;

	void Start () {

		rb = GetComponent<Rigidbody> ();
		HighScore = PlayerPrefs.GetInt("HighScore",0);

	}
	
	// Update is called once per frame
	void Update () {

		if(score > HighScore){
			HighScore = score;
			PlayerPrefs.SetInt("HighScore", HighScore);
		}
			

		if (rb.velocity.magnitude < 2) {
			pSystem.Stop ();
		} else {

			if (!pSystem.isPlaying) {
				pSystem.Play ();
			}
		}
		
	}

	private void FixedUpdate(){

		//Debug.Log ("move");
		if(!isGameOver){
			
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			rb.AddForce (movement * speed);	
		}


   }

	void OnTriggerEnter(Collider other){

		Debug.Log ("in trigger:");

		if (other.gameObject.tag == "star") {
		
			Destroy (other.gameObject);
			score = score + 5;
			txtScore.text = "Score : " + score;

		}

		if (other.gameObject.tag == "bomb") {

			isGameOver = true;
			rb.velocity = Vector3.zero;
			//list.Add (score);
			//list.Sort ();
			//Debug.Log (list.Count);
			//foreach (var item in list)
			//{
			//	Debug.Log (item);
			//}
			//Debug.Log (HighScore);



			rb.isKinematic = true;
			pSystemEnemy.Play ();
			Destroy (other.gameObject,1.0f);
			panel.SetActive (true);


		}

		if (other.gameObject.tag == "plane1" || other.gameObject.tag == "plane2" || other.gameObject.tag == "plane3") {
			
			rb.velocity = Vector3.zero;
			rb.isKinematic = true;
			myScore.text = "Your Score : " + score;

			bestScore.text = "Your Best Score : " + HighScore;

			

			panel2.SetActive (true);



		}

	}


	public void playAgainUI(){
	
		SceneManager.LoadScene ("SceneOne"); 
	
	}





}
